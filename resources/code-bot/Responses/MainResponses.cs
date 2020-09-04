using System.Collections.Generic;
using System.Threading.Tasks;
using CognitiveSearchBot.Utilities;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Responses
{
    public class MainResponses
    {
        public static async Task ReplyWithGreeting(ITurnContext context)
        {
            // Add a greeting
            await context.SendActivityAsync($"Hi, I'm CognitiveSearchBot!");
        }
        public static async Task ReplyWithHelp(ITurnContext context)
        {
            IMessageActivity activity = context.Activity.CreateReply();
            activity.Text = $"I can retrieve cognitive fields from Azure Cognitive Search. To start a new search, respond \"search\"";
            activity.SuggestedActions = HeroCardUtility.InitialSuggestions();
            await context.SendActivityAsync(activity);
        }
        public static async Task ReplyWithResumeTopic(ITurnContext context)
        {
            await context.SendActivityAsync($"What can I do for you?");
        }
        public static async Task ReplyWithConfused(ITurnContext context)
        {
            // Add a response for the user if Regex doesn't know
            // What the user is trying to communicate
            IMessageActivity activity = context.Activity.CreateReply();
            activity.Text = $"I'm sorry, I don't understand.";
            activity.SuggestedActions = HeroCardUtility.InitialSuggestions();
            await context.SendActivityAsync(activity);
        }
    }
}