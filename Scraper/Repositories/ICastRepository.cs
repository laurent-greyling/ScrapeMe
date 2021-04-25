using Scraper.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scraper.Repositories
{
    public interface ICastRepository
    {
        /// <summary>
        /// Add cast memebers to the database retrieved from the API call
        /// </summary>
        /// <param name="cast"></param>
        Task AddRangeAsync(List<Cast> cast);

        /// <summary>
        /// Retreive cast data from database
        /// </summary>
        /// <param name="shows"></param>
        /// <returns></returns>
        IEnumerable<Cast> Retrieve();
    }
}
