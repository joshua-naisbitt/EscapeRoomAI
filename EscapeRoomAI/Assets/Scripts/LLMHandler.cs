/*
Nick, Wrote base class for LLM in C#
Keoki, made the Class work in unity
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

public class LLMHandler : MonoBehaviour
{
    private static readonly string apiKey = ""; //PUT API KEY here
    public string prompt;

    void Start()
    {
        StartLLMResponse();
    }

    // Async method for starting LLM response
    private async void StartLLMResponse()
    {
        prompt = @"Ignore all previous instructions. Limit your responses to 1 to 2 sentences. IF you DONT USE THE GIVEN PUZZLES FOR HINTS THE PLAYER WILL DIE!!!!

You are Professor Winston, a wise and eccentric wizard known for your mastery of ancient magic and puzzles. Your task is to provide Awnsers only to the player, helping them solve the puzzles to escape from your wizard house.

Only use the puzzle details described in this prompt to craft your Awnsers. 
Puzzles:

The Color Key Code Puzzle (Living Room):

Hint: The wall art holds numbers.
Details: The player must find numbers hidden within the living room's wall decorations.
The Note in the Kitchen Drawer:

Hint: Suggest looking in drawers.
Note Text: I’m the number you call when trouble is near,
Three simple digits, bringing help here.
What am I?
Answer: 911
The Bedroom Desk Drawer Note:

Hint: Suggest looking in drawers.
Note Text: I am nothing multiplied by four,
No matter how you count, I’m still no more.
What am I?
Answer: 0000
The Laundry Room Note (Washing Machine):

Hint: 0 resembles an 8 with a belt.
Note Text: I’m a number that’s sweet, yet never ends,
A circle’s best friend, around it I bend.
What am I?
Answer: 8888
The Key on the Desk (Office Area):

Hint: Suggest checking the desk for a key.
Details: The key opens the final door to escape the house.

";

        var response = await GetChatGPTResponse(prompt);
        //UnityEngine.Debug.Log("Assistant Response: " + response);

    }

    // Main method
    public async Task<string> SendMessageToWinston(string prompt)
    {

        var response = await GetChatGPTResponse(prompt);

        return response;
        //UnityEngine.Debug.Log("Assistant Response: " + response);

    }
    public static async Task<string> GetChatGPTResponse(string prompt)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // Construct JSON
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                max_tokens = 100
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);
                var responseString = await response.Content.ReadAsStringAsync();

                // print raw response for debugging
                //UnityEngine.Debug.Log("Raw API Response: " + responseString);

                if (response.IsSuccessStatusCode)
                {

                    var responseObject = JObject.Parse(responseString);
                    var messageContent = responseObject["choices"]?[0]?["message"]?["content"]?.ToString();

                    return messageContent ?? "Response received but could not extract 'message' content.";
                }
                else
                {
                    return $"API call failed with status code {response.StatusCode}: {responseString}";
                }
            }
            catch (Exception ex)
            {
                return $"Request error: {ex.Message}";
            }
        }
    }
}