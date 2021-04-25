using System.Threading.Tasks;

namespace Scraper.Utilities
{
    public interface IMessageClient
    {
        /// <summary>
        /// SendAsync message to Servicebus queue
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendAsync(string message, int id);
    }
}
