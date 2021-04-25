using Microsoft.EntityFrameworkCore;
using Scraper.Entities;
using Scraper.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scraper.Repositories
{
    public class CastRepository : ICastRepository
    {
        private readonly PlayScraperContext _context;

        public CastRepository(PlayScraperContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<Cast> cast)
        {
            Ensure.ArgumentNotNull(cast, nameof(cast));

            _context.Cast.AddRange(cast);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Cast> Retrieve() => _context.Cast.AsNoTracking();
    }
}
