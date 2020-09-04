using CognitiveSearchBot.Utilities;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Models
{
    public class SearchHitStyler
    {
        public void Apply<T>(ref IMessageActivity activity, string prompt, IReadOnlyList<T> options, IReadOnlyList<string> descriptions = null)
        {
            if (options is IList<SearchHit> hits)
            {
                List<CardAction> cardButtons = new List<CardAction>();
                var cards = hits.Select(h =>
                {
                    var card = new HeroCard
                    {
                        Title = WebUtility.UrlDecode(h.FileName).Replace(' ', '_'),
                        Text = h.Description,
                        Buttons = new List<CardAction> { new CardAction()
                    {
                        Value = h.DocumentUrl,
                        Type = "openUrl",
                        Title = "Open Document"
                    } }
                    };
                    if (h.IsImage)
                    {
                        card.Images = new List<CardImage>
                        {
                            new CardImage(h.DocumentUrl)
                        };
                    }
                    return card;
                });

                activity.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                activity.Attachments = cards.Select(c => c.ToAttachment()).ToList();
                activity.SuggestedActions = HeroCardUtility.FollowupSuggestions();
                activity.Text = prompt;
            }
        }
    }
}
