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
        public async Task<IEnumerable<SearchResult>> ListAsync(string query)
        {
            return await DoSearch(query);
        }

        public async Task<IEnumerable<SearchResult>> DoSearch(string query)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("test");
            var client = new ElasticClient(settings);

             var searchResponse = await client.SearchAsync<SearchResult>(s => s
            .AllIndices()
            .From(0)
            .Size(10)
            .Query(q => q
                 .Match(m => m
                    .Field(f => f.Text)
                    .Query($"{query}")
                    )
                )
            );

            var enumerable = searchResponse.Hits.Select(h => h.Source);
            return enumerable;
        }
    }
}
