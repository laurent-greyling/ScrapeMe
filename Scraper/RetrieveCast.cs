using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Scraper.Services;
using System;
using System.Threading.Tasks;

namespace Scraper
{
    public class RetrieveCast
    {
        private readonly ICastService _castService;

        public RetrieveCast(ICastService castService)
        {
            _castService = castService;
        }

        [FunctionName("RetrieveCast")]
        public async Task Run([ServiceBusTrigger("mainscraperqueue", Connection = "SbConnectionString")]Message queueMessage, ILogger log)
        {
            try
            {
                await _castService.AddRangeAsync(queueMessage);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
            }            
        }
    }
}
