using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class SearchHit
    {
        public SearchHit()
        {
            this.PropertyBag = new Dictionary<string, object>();
        }
        public string Title { get; set; }
        public string DocumentUrl
        {
            get
            {
                object blobUri = string.Empty;
                return PropertyBag.TryGetValue("blob_uri", out blobUri) ? blobUri.ToString() : string.Empty;
            }
        }
        public string FileName
        {
            get
            {
                return !string.IsNullOrWhiteSpace(DocumentUrl) ? DocumentUrl.Split('/').Last() : string.Empty;
            }
        }
        public bool ShouldBeModerated
        {
            get
            {
                object result = string.Empty;
                var results = PropertyBag.TryGetValue("needsModeration", out result);
				
				if (result != null)
                    return bool.Parse(result.ToString());

                return false;
            }
        }

        public string Description
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!IsImage)
                {
                    ComposeDetail(sb, "content", (ShouldBeModerated ? "Content Preview - Contains PII Data" : "Content Preview"));
                }
                ComposeArrayDetail(sb, "keyPhrases", "Key Phrases");
                ComposeArrayDetail(sb, "organizations", "Organizations");
                ComposeArrayDetail(sb, "myOcrText", "OCR Text");
                return sb.ToString();
            }
        }

        private void ComposeArrayDetail(StringBuilder sb, string dictionaryEntryName, string displayHeading)
        {
            object items = string.Empty;
            var results = PropertyBag.TryGetValue(dictionaryEntryName, out items) && (items as string[]).Count() > 0 ? $"\r\n## **{displayHeading}**\r\n---\r\n{CreateTruncatedList(items as string[])}" : string.Empty;
            if (!string.IsNullOrWhiteSpace(results))
            {
                sb.AppendLine(results);
            }
        }
        private void ComposeDetail(StringBuilder sb, string dictionaryEntryName, string displayHeading)
        {
            object item = string.Empty;
            var results = PropertyBag.TryGetValue(dictionaryEntryName, out item) && (item as string).Count() > 0 ? $"\r\n## **{displayHeading}**\r\n---\r\n{CreateTruncatedList(new string[] { item as string }, maxTermLength:500)}" : string.Empty;
            if (!string.IsNullOrWhiteSpace(results))
            {
                sb.AppendLine(results);
            }
        }
        public bool IsImage
        {
            get
            {
                return !string.IsNullOrWhiteSpace(FileName) && FilenameIsImage();
            }
        }
        public IDictionary<string, object> PropertyBag { get; set; }
        private bool FilenameIsImage()
        {
            var fileExtension = FileName.Split('.').Last();
            if (!string.IsNullOrWhiteSpace(fileExtension))
            {
                switch (fileExtension.ToLower())
                {
                    case ("png"):
                    case ("jpg"):
                    case ("jpeg"):
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        private string CreateTruncatedList(string[] items, int maxCount = 20, int maxTermLength = 100)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (var item in items)
            {
                var current = item;
                if (item.Length > maxTermLength)
                {
                    current = item.Substring(0, maxTermLength);
                }
                sb.Append($"{current}, ");
                count++;
                if (count >= maxCount)
                {
                    break;
                }
            }
            return sb.ToString().Trim().TrimEnd(',');
        }
    }
}