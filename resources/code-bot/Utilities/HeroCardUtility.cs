using Microsoft.Bot.Schema;
using System.Collections.Generic;

namespace CognitiveSearchBot.Utilities
{
    public class HeroCardUtility
    {
        private static readonly CardAction InitialSearch = new CardAction() { Title = "Perform Search", Type = ActionTypes.ImBack, Value = "search" };
        private static readonly CardAction FollowupSearch = new CardAction() { Title = "Search for another term", Type = ActionTypes.ImBack, Value = "search" };
        private static readonly CardAction ModeratedContent = new CardAction() { Title = "Find moderated content", Type = ActionTypes.ImBack, Value = "moderated content" };
        private static readonly CardAction Help = new CardAction() { Title = "Help", Type = ActionTypes.ImBack, Value = "Help" };
        public static SuggestedActions InitialSuggestions()
        {
            return new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    InitialSearch,
                    ModeratedContent,
                    Help
                }
            };
        }

        public static SuggestedActions FollowupSuggestions()
        {
            return new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    FollowupSearch,
                    ModeratedContent,
                    Help
                }
            };
        }
    }
}
