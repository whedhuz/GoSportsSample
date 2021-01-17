using System.Threading.Tasks;
using GoSportBackEnd.Services.Gateways.Interfaces;
using GoSportBackEnd.Services.Models.Tennis;
using GoSportBackEnd.Services.Services.Interfaces;

namespace GoSportBackEnd.Services.Services
{
    public class GameRetrieverService : IGameRetrieverService
    {
        private readonly ITennisGameGateway _tennisGameGateway;

        public GameRetrieverService(ITennisGameGateway tennisGameGateway)
        {
            _tennisGameGateway = tennisGameGateway;
        }

        public Task<MatchDetails> GetTennisMatchDetails(string id)
        {
            return _tennisGameGateway.GetAsync(id);
        }
    }
}
