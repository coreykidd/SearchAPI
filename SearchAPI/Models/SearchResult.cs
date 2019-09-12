using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAPI.Models
{
    public class SearchResult
    {
        public string Subject { get; set; }
        public string HexEntryId { get; set; }
        //public KeyValuePair<string, IReadOnlyCollection<string>> Highlight { get; set; }
        public string Body { get; set; }

        public SearchResult(string subject, string hexEntryId, string body)
        {
            this.Subject = subject;
            this.HexEntryId = hexEntryId;
            this.Body = body;
        }

        public SearchResult() { }
    }
}
