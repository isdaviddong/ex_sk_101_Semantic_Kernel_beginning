using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;

internal class Program
{
    private static async global::System.Threading.Tasks.Task Main(string[] args)
    {
        //Azure OpenAI 
        var DeployName = "👉模型佈署名稱👈";
        var Endpoint = "https://👉API端點👈.openai.azure.com/";
        var ApiKey = "👉ApiKey👈";

        // Create a new kernel builder
        var builder = Kernel.CreateBuilder()
                    .AddAzureOpenAIChatCompletion(DeployName, Endpoint, ApiKey);
        builder.Plugins.AddFromType<LightPlugin>(); // Add the LightPlugin to the kernel
        Kernel kernel = builder.Build();

        // Create chat history 物件，並且加入
        var history = new ChatHistory();
        history.AddSystemMessage("你是一個親切的智能家庭助理，可以協助用戶回答問題，交談時請使用中文。");

        // Get chat completion service
        var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        // 開始對談
        Console.Write("User > ");
        string? userInput;
        while (!string.IsNullOrEmpty(userInput = Console.ReadLine()))
        {
            // Add user input
            history.AddUserMessage(userInput);

            // Enable auto function calling
            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            // Get the response from the AI
            var result = await chatCompletionService.GetChatMessageContentAsync(
                history,
                executionSettings: openAIPromptExecutionSettings,
                kernel: kernel);

            // Print the results
            Console.WriteLine("Assistant > " + result);

            // Add the message from the agent to the chat history
            history.AddMessage(result.Role, result.Content ?? string.Empty);

            // Get user input again
            Console.Write("User > ");
        }
    }
}

// 
public class LightPlugin
{
    public bool IsOn { get; set; } = false;

    [KernelFunction]
    [Description("取得燈的狀態")]
    public string GetState()
    {
        return IsOn ? "on" : "off";
    }

    [KernelFunction]
    [Description("改變燈的狀態")]
    public string ChangeState(bool newState)
    {
        this.IsOn = newState;
        var state = GetState();

        // Print the state to the console
        Console.WriteLine($"[Light is now {state}]");

        return state;
    }
}
