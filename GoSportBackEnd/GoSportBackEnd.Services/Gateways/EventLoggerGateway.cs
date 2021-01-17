using System.Threading.Tasks;
using GoSportBackEnd.Services.Gateways.Interfaces;
using GoSportBackEnd.Services.Models;
using Microsoft.Extensions.Logging;

namespace GoSportBackEnd.Services.Gateways
{
    public class EventLoggerGateway : IEventLoggerGateway
    {
        private readonly ILogger<EventLoggerGateway> _logger;

        public EventLoggerGateway(ILogger<EventLoggerGateway> logger)
        {
            _logger = logger;
        }

        public Task LogEvent(Event eventObj, bool processSuccessful)
        {
            // Not Implemented due to time constraint
            // Logs the event for replayability;
            if (processSuccessful)
            {
                _logger.LogInformation("Success processing event {@event}", eventObj);
            }
            else
            {
                _logger.LogError("Failed to process event {@event}", eventObj);
            }
            return Task.CompletedTask;
        }
    }
}
