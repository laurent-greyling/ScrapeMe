using Scraper.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scraper.Repositories
{
    public interface IShowsRepository
    {
        /// <summary>
        /// Add shows to the database retrieved from the API call
        /// </summary>
        /// <param name="shows"></param>
        Task AddRangeAsync(List<Shows> shows);

        /// <summary>
        /// Retreive show data from database
        /// </summary>
        /// <param name="shows"></param>
        /// <returns></returns>
        IEnumerable<Shows> Retrieve();

        /// <summary>
        /// Check if list in DB and Api correlate, only take from API where ID does not exist and add these as new shows to DB
        /// </summary>
        /// <param name="shows"></param>
        /// <returns></returns>
        List<Shows> GetNewShows(List<Shows> shows);
    }
}
