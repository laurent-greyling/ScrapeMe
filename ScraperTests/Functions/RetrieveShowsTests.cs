using Moq;
using Scraper;
using Scraper.Services;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.Azure.WebJobs;
using Scraper.Entities;
using System.Collections.Generic;

namespace ScraperTests.Functions
{
    [ExcludeFromCodeCoverage]
    public class RetrieveShowsTests
    {
        private readonly Mock<IShowsService> _mockedShowsService;

        private readonly RetrieveShows _retrieveShows;

        public RetrieveShowsTests()
        {

            _mockedShowsService = new Mock<IShowsService>();

            _mockedShowsService.Setup(x => x.AddRangeAsync(It.IsAny<List<Shows>>())).Verifiable();
            _mockedShowsService.Setup(x => x.AddQueueMessageAsync(It.IsAny<List<Shows>>())).Verifiable();
            _mockedShowsService.Setup(x => x.GetShowsAsync()).Returns(Task.FromResult(It.IsAny<List<Shows>>())).Verifiable();

            _retrieveShows = new RetrieveShows(_mockedShowsService.Object);
        }

        [Fact]
        public async Task RetrieveShows_Run_Executed_AddRangeAsync()
        {
            await _retrieveShows.Run(It.IsAny<TimerInfo>(), It.IsAny<ILogger>());

            _mockedShowsService.Verify(x => x.AddRangeAsync(It.IsAny<List<Shows>>()), Times.Once);
        }

        [Fact]
        public async Task RetrieveShows_Run_Executed_AddQueueMessageAsync()
        {
            await _retrieveShows.Run(It.IsAny<TimerInfo>(), It.IsAny<ILogger>());

            _mockedShowsService.Verify(x => x.AddQueueMessageAsync(It.IsAny<List<Shows>>()), Times.Once);
        }

        [Fact]
        public async Task RetrieveShows_Run_Executed_GetShowsAsync()
        {
            await _retrieveShows.Run(It.IsAny<TimerInfo>(), It.IsAny<ILogger>());

            _mockedShowsService.Verify(x => x.GetShowsAsync(), Times.Once);
        }
    }
}
