using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAPI.Models
{
    public class SearchResult
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ConversationId { get; set; }
    }
}
