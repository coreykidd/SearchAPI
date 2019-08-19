using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchAPI.Models;
using SearchAPI.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchAPI.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        // GET: api/search
        [HttpGet("")]
        public async Task<IEnumerable<SearchResult>> GetAllAsync(string index, string query)
        {            
            var searchResults = await _searchService.ListAsync(index, query);
            return searchResults;
        }
    }
}
