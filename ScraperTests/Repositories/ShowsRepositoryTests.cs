using Microsoft.EntityFrameworkCore;
using Moq;
using Scraper.Entities;
using Scraper.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ScraperTests.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ShowsRepositoryTests
    {
        private readonly Mock<PlayScraperContext> _mockedContext;
        private readonly Mock<DbSet<Shows>> _mockedShows;

        private Shows _show;
        private Shows _show1;
        private IQueryable<Shows> _shows;

        private readonly string _showName = "FakeName";
        private readonly string _showName1 = "FakeName1";

        private readonly ShowsRepository _repository;

        public ShowsRepositoryTests()
        {
            SetupData();

            _mockedContext = new Mock<PlayScraperContext>();
            _mockedShows = new Mock<DbSet<Shows>>();

            _mockedShows.As<IQueryable<Shows>>().Setup(m => m.Provider).Returns(_shows.Provider);
            _mockedShows.As<IQueryable<Shows>>().Setup(m => m.Expression).Returns(_shows.Expression);
            _mockedShows.As<IQueryable<Shows>>().Setup(m => m.ElementType).Returns(_shows.ElementType);
            _mockedShows.As<IQueryable<Shows>>().Setup(m => m.GetEnumerator()).Returns(_shows.GetEnumerator());

            _mockedContext.Setup(x => x.Shows).Returns(_mockedShows.Object);
            _mockedContext.Setup(x => x.Shows.AddRangeAsync(_shows, It.IsAny<CancellationToken>())).Returns(Task.FromResult(_mockedShows.Object));
            _mockedContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            _repository = new ShowsRepository(_mockedContext.Object);
        }

        [Fact]
        public void ShowsRepository_AddRangeAsync_ShowsList_Null_Throws_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddRangeAsync(null));
        }

        [Fact]
        public async Task ShowsRepository_AddRangeAsync_Context_AddRange_Executed()
        {
            await _repository.AddRangeAsync(_shows.ToList());

            _mockedContext.Verify(x => x.Shows.AddRangeAsync(_shows, It.IsAny<CancellationToken>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task ShowsRepository_AddRangeAsync_Context_SaveChanges_Executed()
        {
            await _repository.AddRangeAsync(_shows.ToList());

            _mockedContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce);
        }

        [Fact]
        public void ShowsRepository_RetrieveAsync_Returns_Expected()
        {
            var result = _repository.Retrieve().ToList();

            Assert.Equal(1, result[0].Id);
            Assert.Equal(_showName, result[0].Name);
        }

        [Fact]
        public void ShowsRepository_GetNewShows_Returns_OnlyNewEntries()
        {
            var shows = new List<Shows>
            {
                _show, 
                _show1,
                new Shows
                {
                    Id = 3,
                    Name = "Show3"
                },
                new Shows
                {
                    Id = 4,
                    Name = "Show4"
                }
            };

            var result = _repository.GetNewShows(shows);

            Assert.Equal(2, result.Count);
            Assert.Equal(3, result[0].Id);
            Assert.Equal("Show3", result[0].Name);
        }

        private void SetupData()
        {
            _show = new Shows 
            {
                Id = 1,
                Name = _showName
            };

            _show1 = new Shows
            {
                Id = 2,
                Name = _showName1
            };

            _shows = new List<Shows> { _show, _show1 }.AsQueryable();
        }
    }
}