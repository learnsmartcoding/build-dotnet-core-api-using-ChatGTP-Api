using Microsoft.AspNetCore.Mvc;
using OpenAI.API;
using OpenAI.API.Completions;

namespace ChatGTP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatGTPController : ControllerBase
    {


        private readonly ILogger<ChatGTPController> _logger;

        public ChatGTPController(ILogger<ChatGTPController> logger)
        {
            _logger = logger;
        }


        [HttpPost(Name = "")]
        public async Task<IActionResult> Post(CompletionRequest request)
        {
            // Create an instance of the OpenAI API client
            //Replace your accout key. Goto "https://platform.openai.com/account/api-keys"
            var client = new OpenAIAPI("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");
            if (string.IsNullOrEmpty(request.Model))
            {
                request.Model = "text-davinci-003";
            }

            /*
             *  // 
             // Set the API parameters
            var prompt = "Travel plan in new york city for two days";
            var model = "text-davinci-003";
            var temperature = 0.7;
            var maxTokens = 256;
             new OpenAI.API.Completions.CompletionRequest()
            {
                 Model = model, Prompt =    prompt, Temperature= temperature, MaxTokens = maxTokens,
                  FrequencyPenalty=1, PresencePenalty=0, TopP=1
            }
             * */
            // Call the OpenAI API to generate text
            var response = await client.Completions.CreateCompletionsAsync(request);

            // Handle the API response and print the generated text            
            return new OkObjectResult(response.Completions.Select(s=>s.Text).ToList());
        }
    }
}

/*
 Sample request

{
  "model": "text-davinci-003",
  "multiplePrompts": [
    "string"
  ],
  "prompt": "One day travel plan for couple in NYC",
  "suffix": "string",
  "maxTokens": 256,
  "temperature": 0.8,
  "topP": 1,
  "presencePenalty": 0,
  "frequencyPenalty": 0,
  "numChoicesPerPrompt": 0,
  "logprobs": 0,
  "echo": false,
  "multipleStopSequences": [
    "string"
  ],
  "stopSequence": "string",
  "bestOf": 5,
  "user": "string"
}

*/