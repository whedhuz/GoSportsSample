using System;
using System.Net;
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
        [ProducesResponseType(typeof(SuccessResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(Event eventObj)
        {
            var response = await _eventHandler.ProcessEventAsync(eventObj);

            switch (response)
            {
                case ErrorResponse errorResponse:
                    return BadRequest(errorResponse.ErrorMsg);
                case SuccessResponse _:
                    return Ok(response);
                default:
                {
                    _logger.LogError("Unknown response type for event {@event}", eventObj);
                    throw new ApplicationException("Unknown response type");
                }
            }
        }
    }
}
