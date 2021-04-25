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
    public class CastRepositoryTests
    {
        private readonly Mock<PlayScraperContext> _mockedContext;
        private readonly Mock<DbSet<Cast>> _mockedCast;

        private Cast _cast;
        private Cast _cast1;
        private IQueryable<Cast> _castMembers;

        private readonly string _castName = "FakeName";
        private readonly string _castName1 = "FakeName1";
        private readonly string _bDay = DateTime.UtcNow.Date.ToString();

        private readonly CastRepository _repository;

        public CastRepositoryTests()
        {
            SetupData();

            _mockedContext = new Mock<PlayScraperContext>();
            _mockedCast = new Mock<DbSet<Cast>>();

            _mockedCast.As<IQueryable<Cast>>().Setup(m => m.Provider).Returns(_castMembers.Provider);
            _mockedCast.As<IQueryable<Cast>>().Setup(m => m.Expression).Returns(_castMembers.Expression);
            _mockedCast.As<IQueryable<Cast>>().Setup(m => m.ElementType).Returns(_castMembers.ElementType);
            _mockedCast.As<IQueryable<Cast>>().Setup(m => m.GetEnumerator()).Returns(_castMembers.GetEnumerator());

            _mockedContext.Setup(x => x.Cast).Returns(_mockedCast.Object);
            _mockedContext.Setup(x => x.Cast.AddRangeAsync(_castMembers, It.IsAny<CancellationToken>())).Returns(Task.FromResult(_mockedCast.Object));
            _mockedContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            _repository = new CastRepository(_mockedContext.Object);
        }

        [Fact]
        public void CastRepository_AddRangeAsync_CastMembers_Null_Throws_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddRangeAsync(null));
        }

        [Fact]
        public async Task CastRepository_AddRangeAsync_Context_AddRange_Executed()
        {
            await _repository.AddRangeAsync(_castMembers.ToList());

            _mockedContext.Verify(x => x.Cast.AddRangeAsync(_castMembers, It.IsAny<CancellationToken>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task CastRepository_AddRangeAsync_Context_SaveChanges_Executed()
        {
            await _repository.AddRangeAsync(_castMembers.ToList());

            _mockedContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce);
        }

        [Fact]
        public void CastRepository_RetrieveAsync_Returns_Expected()
        {
            var result = _repository.Retrieve().ToList();

            Assert.Equal(1, result[0].Id);
            Assert.Equal(_castName, result[0].Name);
        }

        private void SetupData()
        {
            _cast = new Cast
            {
                Id = 1,
                ShowId = 1,
                Name = _castName,
                Birthday = _bDay
            };

            _cast1 = new Cast
            {
                Id = 2,
                ShowId = 1,
                Name = _castName1,
                Birthday = _bDay
            };

            _castMembers = new List<Cast> { _cast, _cast1 }.AsQueryable();
        }
    }
}
