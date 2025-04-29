using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;

IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string? key = config["OpenAIKey"];
string? model = config["ModelName"];

IChatClient client = new ChatClientBuilder(new OpenAIClient(key).GetChatClient(model ?? "gpt-4o").AsIChatClient())
    .UseFunctionInvocation() //Use functions
    .Build();

var chatOptions = new ChatOptions
{
    Tools = [AIFunctionFactory.Create((string location, string unit) =>
    {
        //Here is where you would call another api to get the weather 
        //or whatever
        return "Periods of rain or drizzle, 15 C";
    },
    name: "get_current_weather",
    description: "Get the current weather in a given location")]
};


List<ChatMessage> chatHistory = [new(ChatRole.System, """
    You are a hiking enthusiast who helps people discover fun hikes in their area. You are upbeat and friendly.
    """)];

// Weather conversation relevant to the registered function.
chatHistory.Add(new ChatMessage(ChatRole.User,
    "I live in Montreal and I'm looking for a moderate intensity hike. What's the current weather like?"));
Console.WriteLine($"{chatHistory.Last().Role} >>> {chatHistory.Last()}");

ChatResponse response = await client.GetResponseAsync(chatHistory, chatOptions);
Console.WriteLine($"Assistant >>> {response.Text}");