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
    public class TennisGameEventProcessor : IEventProcessor
    {
        private readonly ILogger<TennisGameEventProcessor> _logger;
        private readonly IEventLoggerGateway _eventLoggerGateway;
        private readonly ITennisGameGateway _tennisGameGateway;

        public string[] EventTypes = new[]
        {
            // Sample of important events
            // I take the less granular events as this is a startup project
            // and to minimise selectable buttons as the employees are not tech savvy
            "game.tennis.changeserver",
            //"game.tennis.score",
            //"game.tennis.setwon",
            //"game.tennis.gamewon",
            //"game.tennis.selectserver",
            //"game.tennis.gamecancelled",
        };

        public TennisGameEventProcessor(ILogger<TennisGameEventProcessor> logger,
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
                    case "game.tennis.changeserver":
                    {
                        var tennisEventObject = JsonSerializer.Deserialize<TennisEventObject>(eventObj.ContentJson);
                        if (tennisEventObject == null)
                        {
                            _logger.LogError("Unable to convert event object {@event}", eventObj);
                            await _eventLoggerGateway.LogEvent(eventObj, false);
                            return new ErrorResponse
                            {
                                ErrorMsg = "Unable to process event object"
                            };
                        }

                        var responseObj = await RunChangeServerAsync(tennisEventObject.GameId);
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

        private async Task<MatchDetails> RunChangeServerAsync(string matchId)
        {
            var matchDetails = await _tennisGameGateway.GetAsync(matchId);
            switch (matchDetails.ServingPlayer)
            {
                case 1:
                case 2:
                    matchDetails.ServingPlayer = matchDetails.ServingPlayer == 1 ? 2 : 1;
                    var updatedDetails = await _tennisGameGateway.UpdateAsync(matchDetails);
                    return updatedDetails;
                    
                default:
                    return matchDetails;
            }
        }
    }
}
