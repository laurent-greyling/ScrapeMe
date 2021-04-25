using System.Threading.Tasks;

namespace Scraper.Utilities
{
    /// <summary>
    /// Extract HTTPClient calls into this utility as this is code I want to exclude from testing
    /// and also allows me to have this class as a single responsibility towards rest calls
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Retreive information from api
        /// </summary>
        /// <returns>json string</returns>
        Task<string> RetrieveAsync(string apiPath);
    }
}
