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
           //.Query(q => q
           //     .Match(m => m
           //        .Field(f => f.Body)
           //        .Query(query)
           //        )
           //    )
           .Query(qry => qry
               .Bool(b => b
               .Must(m => m
                   .QueryString(qs => qs
                       .DefaultField("_all")
                       .Query(query))))));
            );

            var enumerable = searchResponse.Hits.Select(h => h.Source);
            return enumerable;
        }
    }
}
