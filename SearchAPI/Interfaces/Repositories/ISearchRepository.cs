using System.Collections.Generic;
using System.Threading.Tasks;
using SearchAPI.Models;

namespace SearchAPI.Interfaces.Repositories
{
    public interface ISearchRepository
    {
        Task<IEnumerable<SearchResult>> ListAsync(string query);
    }
}