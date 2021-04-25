using Newtonsoft.Json;
using Scraper.Entities;
using Scraper.Repositories;
using Scraper.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Scraper.Services
{
    public class ShowsService : IShowsService
    {
        private readonly IShowsRepository _showsRepository;
        private readonly IRestClient _client;
        private readonly IMessageClient _messageClient;

        public ShowsService(IShowsRepository showsRepository,
            IRestClient client,
            IMessageClient messageClient)
        {
            _showsRepository = showsRepository;
            _client = client;
            _messageClient = messageClient;
        }

        public async Task AddQueueMessageAsync(List<Shows> shows)
        {
            foreach (var show in shows)
            {
                var apiUri = $"shows/{show.Id}/cast";
                await _messageClient.SendAsync(apiUri, show.Id);
            }
        }

        public async Task AddRangeAsync(List<Shows> shows)
        {
            await _showsRepository.AddRangeAsync(shows);
        }

        public async Task<List<Shows>> GetShowsAsync()
        {
            var result = await _client.RetrieveAsync("shows");
            var shows = JsonConvert.DeserializeObject<List<Shows>>(result);

            return _showsRepository.GetNewShows(shows);
        }
    }
}
