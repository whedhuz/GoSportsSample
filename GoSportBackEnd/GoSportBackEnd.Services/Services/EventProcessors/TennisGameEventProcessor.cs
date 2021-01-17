using System;
using System.Linq;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Gateways.Interfaces;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoSportBackEnd.Services.Services.EventProcessors
{
    public class TennisGameEventProcessor : IEventProcessor
    {
        private readonly ILogger<TennisGameEventProcessor> _logger;
        private readonly IEventLoggerGateway _eventLoggerGateway;

        public string[] EventTypes = new[]
        {
            // Sample of important events
            // I take the less granular events as this is a startup project
            // and to minimise selectable buttons as the employees are not tech savvy
            "game.tennis.changeserver",
            //"game.tennis.score",
            //"game.tennis.setwon",
            //"game.tennis.gamewon",
        };

        public TennisGameEventProcessor(ILogger<TennisGameEventProcessor> logger, IEventLoggerGateway eventLoggerGateway)
        {
            _logger = logger;
            _eventLoggerGateway = eventLoggerGateway;
        }

        public bool CanProcess(string eventType)
        {
            return EventTypes.Contains(eventType);
        }

        public async Task<EventResponse> ProcessEventAsync(Event eventObj)
        {
            try
            {
                _logger.LogInformation("Processing event {@eventType}", eventObj.Type);
                switch (eventObj.Type)
                {
                    case "game.tennis.changeserver":
                    {
                        break;
                    }

                    default:
                        throw new ApplicationException("Unhandled tennis game event type");
                }
                
                await _eventLoggerGateway.LogEvent(eventObj, true);
                return new SuccessResponse
                {
                    Id = new Guid()
                };
            }
            catch (Exception e)
            {
                await _eventLoggerGateway.LogEvent(eventObj, false);
                throw;
            }
        }
    }
}
