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

        public async Task ProcessEventAsync(Event eventObj)
        {
            var applicableProcessor = _processors.SingleOrDefault(p => p.CanProcess(eventObj.Type));
            if (applicableProcessor != null) 
            {
                await applicableProcessor.ProcessEvent(eventObj);
            }
        }
    }
}
