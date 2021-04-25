using Microsoft.Azure.ServiceBus;
using Scraper.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Services
{
    public interface ICastService
    {
        /// <summary>
        /// Retreive Cast from API and add to Db
        /// </summary>
        Task AddRangeAsync(Message message);
    }
}
