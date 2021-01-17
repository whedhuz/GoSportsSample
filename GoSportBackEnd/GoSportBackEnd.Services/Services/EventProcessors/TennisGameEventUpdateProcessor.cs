using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Gateways.Interfaces;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Models.Tennis;
using GoSportBackEnd.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoSportBackEnd.Services.Services.EventProcessors
{
    public class TennisGameEventUpdateProcessor : IEventProcessor
    {
        private readonly ILogger<TennisGameEventUpdateProcessor> _logger;
        private readonly IEventLoggerGateway _eventLoggerGateway;
        private readonly ITennisGameGateway _tennisGameGateway;

        public string[] EventTypes = new[]
        {
            // Can possible add insert and delete in the future
            "game.tennis.event.update",
        };

        public TennisGameEventUpdateProcessor(ILogger<TennisGameEventUpdateProcessor> logger,
            IEventLoggerGateway eventLoggerGateway, ITennisGameGateway tennisGameGateway)
        {
            _logger = logger;
            _eventLoggerGateway = eventLoggerGateway;
            _tennisGameGateway = tennisGameGateway;
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
                    case "game.tennis.event.update":
                    {
                        var updateObject = JsonSerializer.Deserialize<TennisEventUpdateObject>(eventObj.ContentJson);
                        if (updateObject == null)
                        {
                            _logger.LogError("Unable to convert event update object {@event}", eventObj);
                            await _eventLoggerGateway.LogEvent(eventObj, false);
                            return new ErrorResponse
                            {
                                ErrorMsg = "Unable to process event object"
                            };
                        }

                        var responseObj = await RunUpdateEventAsync(updateObject);
                        await _eventLoggerGateway.LogEvent(eventObj, true);
                        return new SuccessResponse
                        {
                            Id = new Guid(),
                            ResponseObj = responseObj
                        };
                    }

                    default:
                        throw new ApplicationException("Unhandled tennis game event type");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Unhandled exception for event {@event}, exception {@ex}", eventObj, e);
                await _eventLoggerGateway.LogEvent(eventObj, false);
                throw;
            }
        }

        private async Task<MatchDetails> RunUpdateEventAsync(TennisEventUpdateObject updateObject)
        {
            // Not implemented due to time constraint
            throw new NotImplementedException();

            // 1. Search the logged event sequence based in gameId 
            // 2. Based on the action: Update/Delete/Insert new event to the sequence of events
            // 3. Recalculate the final outcome for the match
            // 4. Return the recalculated outcome

            // Issue to solve: traceability of changing the logged event 
            // Possible solution: keep the logged event and the sequence of events to produce the outcome separate
        }
    }
}
