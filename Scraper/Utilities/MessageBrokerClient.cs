using Microsoft.Azure.ServiceBus;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Utilities
{
    [ExcludeFromCodeCoverage]
    public class MessageBrokerClient : IMessageClient
    {
        private readonly string _serviceBusConnection = Environment.GetEnvironmentVariable("SbConnectionString");
        private readonly string _serviceQueueName = Environment.GetEnvironmentVariable("QueueName");

        public QueueClient Client => new QueueClient(_serviceBusConnection, _serviceQueueName);

        public async Task SendAsync(string message, int id)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var brokeredMessage = new Message
            {
                Body = body,
                ContentType = "text/plain"
            };

            brokeredMessage.UserProperties.Add("id", id);

            await Client.SendAsync(brokeredMessage);
        }
    }
}
