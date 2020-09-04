using Microsoft.Azure.Search.Models;
using System.Linq;

namespace Models
{
    public class ImageMapper
    {
        public static SearchHit ToSearchHit(SearchResult hit)
        {
            var searchHit = new SearchHit();
            // Retrieves fields from Cognitive Search.
            hit.Document.ToList().ForEach(x => searchHit.PropertyBag.Add(x.Key, x.Value));

            //var description = "𝐂𝐨𝐠𝐧𝐢𝐭𝐢𝐯𝐞 𝐊𝐞𝐲 𝐏𝐡𝐫𝐚𝐬𝐞𝐬: " +
            //    System.Environment.NewLine +
            //    keyPhrases + 
            //    System.Environment.NewLine +
            //    "𝐎𝐫𝐠𝐚𝐧𝐢𝐳𝐚𝐭𝐢𝐨𝐧𝐬 𝐈𝐝𝐞𝐧𝐭𝐢𝐟𝐢𝐞𝐝: " +
            //    System.Environment.NewLine +
            //    organizations + 
            //    System.Environment.NewLine 
            //    ;        

            return searchHit;
        }

    }
}