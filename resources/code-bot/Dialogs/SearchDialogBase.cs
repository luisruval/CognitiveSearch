using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Models;
using Responses;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveSearchBot.Dialogs
{
    public class SearchDialogBase
    {
        // Add search related tasks
        // These tasks will actually configure the client and connection to your
        // specific index. We'll call the service with the searchText, and process
        // the results in a way that looks nice in a bot
        public async Task ExecuteSearchAsync(ITurnContext context, string searchText, string filter = "")
        {
            ISearchIndexClient indexClientForQueries = CreateSearchIndexClient();
            // For more examples of calling search with SearchParameters, see
            // https://github.com/Azure-Samples/search-dotnet-getting-started/blob/master/DotNetHowTo/DotNetHowTo/Program.cs.  
            // Call the search service and store the results
            DocumentSearchResult results = await indexClientForQueries.Documents.SearchAsync(searchText, new SearchParameters { Filter = filter });
            await SendResultsAsync(context, searchText, results);
        }

        public async Task SendResultsAsync(ITurnContext context, string searchText, DocumentSearchResult results)
        {
            IMessageActivity activity = context.Activity.CreateReply();
            // if the search returns no results
            if (results.Results.Count == 0)
            {
                await SearchResponses.ReplyWithNoResults(context, searchText);
            }
            else // this means there was at least one hit for the search
            {
                // create the response with the result(s) and send to the user
                // the response is formatted with the files within the Models folder
                SearchHitStyler searchHitStyler = new SearchHitStyler();
                searchHitStyler.Apply(
                    ref activity,
                    "Here are the results that I found:",
                    results.Results.Select(r => ImageMapper.ToSearchHit(r)).ToList().AsReadOnly());
                // send the response to the user
                await context.SendActivityAsync(activity);
            }
        }
        public ISearchIndexClient CreateSearchIndexClient()
        {
            // Configure the search service and establish a connection, call it in StartAsync()
            return new SearchIndexClient(Constants.SearchServiceName, Constants.TargetIndexName, new SearchCredentials(Constants.SearchServiceApiKey)); ;
        }
    }
}
