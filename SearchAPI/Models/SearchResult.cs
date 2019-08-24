using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAPI.Models
{
    public class SearchResult
    {
        public string Subject { get; set; }
        public string ConversationId { get; set; }
        //public KeyValuePair<string, IReadOnlyCollection<string>> Highlight { get; set; }
        public string Body { get; set; }

        public SearchResult(string subject, string conversationId, string body)
        {
            this.Subject = subject;
            this.ConversationId = conversationId;
            this.Body = body;
        }

        public SearchResult() { }
    }
}
