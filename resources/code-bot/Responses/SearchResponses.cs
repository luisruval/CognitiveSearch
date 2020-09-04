using CognitiveSearchBot.Utilities;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading.Tasks;

namespace Responses
{
    public class SearchResponses
    {
        // add a task called "ReplyWithSearchRequest"
        // it should take in the context and ask the
        // user what they want to search for
        internal static async Task ReplyWithSearchRequest(ITurnContext context)
        {
            await context.SendActivityAsync($"What would you like to search for?");
        }
        internal static async Task ReplyWithSearchConfirmation(ITurnContext context, string utterance)
        {
            await context.SendActivityAsync($"Ok, searching for \"" + utterance + "\"...");
        }
        internal static async Task ReplyWithNoResults(ITurnContext context, string utterance)
        {
            IMessageActivity activity = context.Activity.CreateReply();
            activity.Text = "There were no results found for \"" + utterance + "\".";
            activity.SuggestedActions = HeroCardUtility.FollowupSuggestions();
            await context.SendActivityAsync(activity);
        }

        internal static async Task ReplyWithSearchForModeratedContentConfirmation(ITurnContext context)
        {
            await context.SendActivityAsync($"Ok, searching for content that should be moderated...");
        }
    }
}