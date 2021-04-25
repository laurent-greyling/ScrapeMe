using Moq;
using Scraper.Entities;
using Scraper.Repositories;
using Scraper.Services;
using Scraper.Utilities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace ScraperTests.Services
{
    [ExcludeFromCodeCoverage]
    public class ShowsServiceTests
    {
        private readonly Mock<IShowsRepository> _mockedRepository;
        private readonly Mock<IRestClient> _mockedClient;
        private readonly Mock<IMessageClient> _mockedMessageClient;

        private readonly string _fakeApiUri = "shows";
        private readonly string _fakeMessage = "fakeMessage";
        private readonly string _fakeData = @"[
    {
        'id': 1,
        'name': 'FakeName'
    },
    {
        'id': 4,
        'name': 'FakeName2',
    }
]";

        private readonly List<Shows> _shows = new List<Shows>
        {
            new Shows
            {
                Id = 1,
                Name = "FakeName"
            },
            new Shows
            {
                Id = 4,
                Name = "FakeName2"
            }
        };

        private readonly ShowsService _showService;

        public ShowsServiceTests()
        {
            _mockedRepository = new Mock<IShowsRepository>();
            _mockedClient = new Mock<IRestClient>();
            _mockedMessageClient = new Mock<IMessageClient>();

            _mockedClient.Setup(x => x.RetrieveAsync(_fakeApiUri)).ReturnsAsync(_fakeData).Verifiable();
            _mockedRepository.Setup(x => x.AddRangeAsync(It.IsAny<List<Shows>>())).Verifiable();
            _mockedRepository.Setup(x => x.GetNewShows(_shows)).Returns(_shows).Verifiable();
            _mockedMessageClient.Setup(x => x.SendAsync(It.IsAny<string>(), It.IsAny<int>())).Verifiable();

            _showService = new ShowsService(_mockedRepository.Object, _mockedClient.Object, _mockedMessageClient.Object);
        }

        [Fact]
        public async Task ShowsService_GetShowsAsync_Returns_Shows()
        {
            await _showService.GetShowsAsync();

            _mockedClient.Verify(x => x.RetrieveAsync(_fakeApiUri), Times.Once);
            _mockedRepository.Verify(x => x.GetNewShows(_shows), Times.AtMostOnce);
        }

        [Fact]
        public async Task ShowsService_RetrieveAsync_Repository_AddRange_Executed()
        {
            await _showService.AddRangeAsync(It.IsAny<List<Shows>>());

            _mockedRepository.Verify(x => x.AddRangeAsync(It.IsAny<List<Shows>>()), Times.Once);
        }

        [Fact]
        public async Task ShowsService_AddQueueMessageAsync_Send_Message()
        {
            await _showService.AddQueueMessageAsync(_shows);

            _mockedMessageClient.Verify(x => x.SendAsync(It.IsAny<string>(), It.IsAny<int>()), Times.AtLeastOnce);
        }
    }
}
