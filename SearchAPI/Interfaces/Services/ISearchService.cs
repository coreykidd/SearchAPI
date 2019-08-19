using System.Collections.Generic;
using System.Threading.Tasks;
using SearchAPI.Models;

namespace SearchAPI.Interfaces.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchResult>> ListAsync(string index, string query);
    }
}