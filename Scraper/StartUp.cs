using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Scraper;
using Scraper.Entities;
using Scraper.Repositories;
using Scraper.Services;
using Scraper.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

[assembly: FunctionsStartup(typeof(StartUp))]
namespace Scraper
{
    [ExcludeFromCodeCoverage]
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = builder.GetContext().Configuration;

            //allow 10 parallel http calls, and up to 20 http calls to 'queue' for an execution slot (thumbsuck values)
            var throttler = Policy.BulkheadAsync<HttpResponseMessage>(10, 20);

            builder.Services.AddHttpClient<IRestClient, RestClient>(client =>
            {
                var apiUri = configuration.GetSection("ApiUri");
                client.BaseAddress = new Uri(apiUri.Value);
            }).AddPolicyHandler(throttler);

            builder.Services.AddScoped<IMessageClient, MessageBrokerClient>()
            .AddScoped<IShowsService, ShowsService>()
            .AddScoped<ICastService, CastService>()
            .AddScoped<IShowsRepository, ShowsRepository>()
            .AddScoped<ICastRepository, CastRepository>()
            .AddScoped(serviceProvider => 
                new PlayScraperContext());

            builder.Services.AddLogging();
        }
    }
}
