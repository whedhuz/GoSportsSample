using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Models.Tennis;
using GoSportBackEnd.Services.Services.Interfaces;

namespace GoSportBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameRetrieverService _retrieverService;

        public GameController(ILogger<GameController> logger, IGameRetrieverService retrieverService)
        {
            _logger = logger;
            _retrieverService = retrieverService;
        }

        [HttpGet]
        [Route("Tennis")]
        [ProducesResponseType(typeof(MatchDetails), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetTennisMatch([FromQuery] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Invalid blank or null id");
                return BadRequest("Invalid tennis match id");
            }

            var tennisMatch = await _retrieverService.GetTennisMatchDetails(id);
            if (tennisMatch == null)
            {
                _logger.LogError("Cannot find tennis match with id {@id}", id);
                return BadRequest("Cannot find tennis match");
            }

            return Ok(tennisMatch);
        }
    }
}
