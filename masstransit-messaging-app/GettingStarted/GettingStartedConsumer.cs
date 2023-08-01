using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GettingStarted
{
    public class GettingStartedConsumer :
     IConsumer<Contracts.GettingStarted>
    {
        readonly ILogger<GettingStartedConsumer> _logger;

        public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Contracts.GettingStarted> context)
        {
            _logger.LogInformation("Consumer 1 - Received Text: {Text}", context.Message.Value);
            return Task.CompletedTask;
        }
    }
}
