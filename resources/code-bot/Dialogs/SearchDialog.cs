using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.CognitiveSearchBot;
using Responses;
using System.Threading;
using System.Threading.Tasks;

namespace CognitiveSearchBot.Dialogs
{
    public class SearchDialog : SearchDialogBase
    {
        private readonly BotAccessors _accessors;
        public static string DialogName = "searchDialog";
        public SearchDialog(BotAccessors botAccessors)
        {
            _accessors = botAccessors;
        }

        public WaterfallDialog Build()
        {
            return new WaterfallDialog(DialogName, new WaterfallStep[] { SearchRequestAsync, SearchAsync });
        }
        private async Task<DialogTurnResult> SearchRequestAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Check if a user has already started searching, and if you know what to search for
            var state = await _accessors.BotState.GetAsync(stepContext.Context);

            // If they're just starting to search for photos
            if (state.Searching == "no")
            {
                // Update the searching state
                state.Searching = "yes";
                // Save the new state into the conversation state.
                await _accessors.ConversationState.SaveChangesAsync(stepContext.Context);
                // Prompt the user for what they want to search for.
                // Instead of using SearchResponses.ReplyWithSearchRequest,
                // we're experimenting with using text prompts
                return await stepContext.PromptAsync("searchPrompt", new PromptOptions { Prompt = MessageFactory.Text("What would you like to search for?") }, cancellationToken);
            }
            else // This means they just told us what they want to search for
                // Go to the next step in the dialog, which is "SearchAsync"
                return await stepContext.NextAsync();
        }
        private async Task<DialogTurnResult> SearchAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Add state so we can update it throughout the turn
            var state = await _accessors.BotState.GetAsync(stepContext.Context);
            // If we haven't stored what they want to search for
            if (state.Search == "")
            {
                // Store it and update the ConversationState
                state.Search = (string)stepContext.Result;
                await _accessors.ConversationState.SaveChangesAsync(stepContext.Context);
            }
            var searchText = state.Search;
            // Confirm with the user what you're searching for
            await SearchResponses.ReplyWithSearchConfirmation(stepContext.Context, searchText);
            // Process the search request and send the results to the user
            await ExecuteSearchAsync(stepContext.Context, searchText);

            // Clear out Search or future searches, set the searching state to no,
            // update the conversation state
            state.Search = "";
            state.Searching = "no";
            await _accessors.ConversationState.SaveChangesAsync(stepContext.Context);

            return await stepContext.EndDialogAsync();
        }
    }
}
