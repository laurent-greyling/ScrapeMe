using Scraper.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scraper.Services
{
    public interface IShowsService
    {
        /// <summary>
        /// Retreive shows from API and Add to DB
        /// </summary>
        Task AddRangeAsync(List<Shows> shows);

        /// <summary>
        /// Add show id to a queue message
        /// </summary>
        /// <returns></returns>
        Task AddQueueMessageAsync(List<Shows> shows);

        /// <summary>
        /// Get the shows not present in the database
        /// </summary>
        /// <returns></returns>
        Task<List<Shows>> GetShowsAsync();
    }
}
