using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GettingStarted
{
    public class GettingStartedConsumer2 :
     IConsumer<Contracts.GettingStarted>
    {
        readonly ILogger<GettingStartedConsumer2> _logger;

        public GettingStartedConsumer2(ILogger<GettingStartedConsumer2> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Contracts.GettingStarted> context)
        {
            _logger.LogInformation("Consumer 2 - Received Text: {Text}", context.Message.Value);
            return Task.CompletedTask;
        }
    }
}
