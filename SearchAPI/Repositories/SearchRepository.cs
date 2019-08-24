using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using SearchAPI.Interfaces.Repositories;
using SearchAPI.Models;

namespace SearchAPI.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        public async Task<IEnumerable<SearchResult>> ListAsync(string index, string query)
        {
            return await DoSearch(index, query);
        }

        public async Task<IEnumerable<SearchResult>> DoSearch(string index, string query)
        {
            var settings = new ConnectionSettings()
                .DefaultMappingFor<SearchResult>(m => m
                .IndexName("emails")
            );
            var client = new ElasticClient(settings);

            var searchResponse = await client.SearchAsync<SearchResult>(s => s
           .Index(index)
           .From(0)
           .Size(10)
           .Source(src => src
            .Includes(i => i
                .Fields(
                    p => p.Subject,
                    p => p.ConversationId
                    )))
            .Query(q => q
                .QueryString(qs => qs
                    .DefaultField("*")
                    .Query(query)))
            .Highlight(h => h
                .PreTags("<mark>")
                .PostTags("</mark>")
                .Fields(fs => fs
                    .Field(f => f.Body)
                    .NumberOfFragments(1)
                    .FragmentSize(200)))
            );

            List<SearchResult> searchResults = new List<SearchResult>();
            //There's almost certainly a way to do this in LINQ, but I can't figure it out at the moment
            //Needs to deserialize metadata and highlighted body to object in one call
            foreach (var hit in searchResponse.Hits)
            {
                string subject = hit.Source.Subject;
                string conversationId = hit.Source.ConversationId;
                string body = "";
                foreach (var highlightCollection in hit.Highlight)
                {
                    foreach(var highlight in highlightCollection.Value)
                    {
                        //Right now API only handles a single hit highlight, could easily be expanded later
                        body = highlight;
                    }
                }
                searchResults.Add(new SearchResult(subject, conversationId, body));
            }
            return searchResults;
        }
    }
}
