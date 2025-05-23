﻿using Microsoft.Extensions.AI;

//https://learn.microsoft.com/en-us/dotnet/ai/quickstarts/create-assistant?pivots=openai
var chatClient =
    new OllamaChatClient(new Uri("http://localhost:11434/"), "qwen3:1.7b");

// Start the conversation with context for the AI model
List<ChatMessage> chatHistory = [];

while (true)
{
    // Get user prompt and add to chat history
    Console.WriteLine("Your prompt:");
    var userPrompt = Console.ReadLine();
    chatHistory.Add(new ChatMessage(ChatRole.User, userPrompt));

    // Stream the AI response and add to chat history
    Console.WriteLine("AI Response:");
    var response = "";
    await foreach (var item in
        chatClient.GetStreamingResponseAsync(chatHistory))
    {
        Console.Write(item.Text);
        response += item.Text;
    }
    chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
    Console.WriteLine();
}