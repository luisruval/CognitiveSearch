using Microsoft.Bot.Builder.Dialogs;
using Microsoft.CognitiveSearchBot;
using Responses;
using System.Threading;
using System.Threading.Tasks;

namespace CognitiveSearchBot.Dialogs
{
    public class ModeratedContentSearchDialog : SearchDialogBase
    {
        private readonly BotAccessors _accessors;
        public static string DialogName = "moderatedContentSearchDialog";
        public ModeratedContentSearchDialog(BotAccessors botAccessors)
        {
            _accessors = botAccessors;
        }

        public WaterfallDialog Build()
        {
            return new WaterfallDialog(DialogName, new WaterfallStep[] { SearchForModeratedContentRequestAsync });
        }
        private async Task<DialogTurnResult> SearchForModeratedContentRequestAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await SearchResponses.ReplyWithSearchForModeratedContentConfirmation(stepContext.Context);

            await ExecuteSearchAsync(stepContext.Context, "*", "needsModeration eq true");
            return await stepContext.EndDialogAsync();
        }
    }
}
