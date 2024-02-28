## Semantic Kernel beginning

這段程式碼是用 C# 寫的，它使用了 OpenAI 的 API 來創建一個 AI 聊天機器人。這個機器人可以與使用者進行對話，並可以直接呼叫一些特定的函數來執行特定的任務。  

首先，程式碼定義了一個名為 LightPlugin 的類別。這個類別有兩個方法：GetState 和 ChangeState。這兩個方法都被標記為 KernelFunction，這意味著它們可以被 AI 呼叫。 

然後，程式碼創建了一個 Kernel 的物件。這個 Kernel 包含了一個 OpenAI 的ChatCompletion服務，以及剛剛定義的 LightPlugin。  

最後，程式碼使用 Kernel 的 ExecuteAsync 方法來開始與 AI 的對話。這個方法接受一個 OpenAIPromptExecutionSettings 物件作為參數，這個物件用於配置對話的行為。  

這段程式碼的主要目的是創建一個可以與使用者進行對話的 AI 聊天機器人，並讓這個機器人能夠呼叫 LightPlugin 的方法來執行開關燈的任務。  

### 參考文章:
https://studyhost.blogspot.com/2024/02/semantic-kernel.html

YT: 
https://youtu.be/UN2i6FYlOiE
