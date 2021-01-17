using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoSportBackEnd.Services.Services
{
    public class EventHandler : IEventHandler
    {
        private readonly ILogger<EventHandler> _logger;
        private readonly IEnumerable<IEventProcessor> _processors;

        public EventHandler(ILogger<EventHandler> logger, IEnumerable<IEventProcessor> processors)
        {
            _logger = logger;
            _processors = processors;
        }

        public async Task<EventResponse> ProcessEventAsync(Event eventObj)
        {
            var applicableProcessor = _processors.SingleOrDefault(p => p.CanProcess(eventObj.Type));
            EventResponse response;
            if (applicableProcessor != null) 
            {
                response = await applicableProcessor.ProcessEvent(eventObj);
            }
            else
            {
                _logger.LogError("Unhandled EventType {@eventObj}", eventObj);
                response = new ErrorResponse
                {
                    ErrorMsg = "Unhandled Event type"
                };
            }

            return response;
        }
    }
}
