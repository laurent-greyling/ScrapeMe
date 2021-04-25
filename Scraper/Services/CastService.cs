using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Scraper.Entities;
using Scraper.Models;
using Scraper.Repositories;
using Scraper.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        private readonly IRestClient _client;

        public CastService(ICastRepository castRepository,
            IRestClient client)
        {
            _castRepository = castRepository;
            _client = client;
        }

        public async Task AddRangeAsync(Message message)
        {
            var castMembers = await HandleMessageAsync(message);
            await _castRepository.AddRangeAsync(castMembers);
        }

        private async Task<List<Cast>> HandleMessageAsync(Message message)
        {
            var apiUri = Encoding.UTF8.GetString(message.Body);
            var showId = message.UserProperties.FirstOrDefault(x => x.Key == "id").Value;

            var castJson = await _client.RetrieveAsync(apiUri);
            var castMembers = JsonConvert.DeserializeObject<List<PersonModel>>(castJson);

            return castMembers.Select(x =>
            {
                return new Cast 
                {
                    CastShowId = $"{ x.Person.Id }_{ showId }",
                    Id =  x.Person.Id,
                    ShowId = (int)showId,
                    Name = x.Person.Name,
                    Birthday = x.Person.Birthday
                };
            }).ToList();
        }
    }
}
