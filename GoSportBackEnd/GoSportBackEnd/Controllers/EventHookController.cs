using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Services.Interfaces;

namespace GoSportBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventHookController : ControllerBase
    {

        private readonly ILogger<EventHookController> _logger;
        private readonly IEventHandler _eventHandler;

        public EventHookController(ILogger<EventHookController> logger, IEventHandler eventHandler)
        {
            _logger = logger;
            _eventHandler = eventHandler;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Event eventObj)
        {
            await _eventHandler.ProcessEventAsync(eventObj);
            return Ok();
        }
    }
}
