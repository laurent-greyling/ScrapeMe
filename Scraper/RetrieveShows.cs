using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Scraper.Services;

namespace Scraper
{
    public class RetrieveShows
    {
        private readonly IShowsService _showsService;

        public RetrieveShows(IShowsService showsService)
        {
            _showsService = showsService;
        }

        /// <summary>
        /// Check for updated list of shows every 3 hours.
        /// </summary>
        /// <param name="timer">0 0 */3 * * *</param>
        /// <param name="log"></param>
        [FunctionName("RetrieveShows")]
        public async Task Run([TimerTrigger("0 0 */3 * * *")]TimerInfo timer, ILogger log)
        {
            try
            {
                var shows = await _showsService.GetShowsAsync();
                await _showsService.AddRangeAsync(shows);
                await _showsService.AddQueueMessageAsync(shows);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
            }           
        }
    }
}
