using System.Collections.Generic;
using System.Threading.Tasks;
using SearchAPI.Interfaces.Services;
using SearchAPI.Models;
using SearchAPI.Interfaces.Repositories;

namespace SearchAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            this._searchRepository = searchRepository;
        }

        public async Task<IEnumerable<SearchResult>> ListAsync(string query)
        {
            return await _searchRepository.ListAsync(query);
        }
    }
}