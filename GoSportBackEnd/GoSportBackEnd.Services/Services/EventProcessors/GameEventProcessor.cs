using System;
using System.Linq;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoSportBackEnd.Services.Services.EventProcessors
{
    public class GameEventProcessor : IEventProcessor
    {
        private readonly ILogger<GameEventProcessor> _logger;

        public string[] EventTypes = new[]
        {
            "sample"
        };

        public GameEventProcessor(ILogger<GameEventProcessor> logger)
        {
            _logger = logger;
        }

        public bool CanProcess(string eventType)
        {
            return EventTypes.Contains(eventType);
        }

        public async Task<EventResponse> ProcessEvent(Event eventObj)
        {
            return new SuccessResponse
            {
                Id = new Guid()
            };
        }
    }
}
