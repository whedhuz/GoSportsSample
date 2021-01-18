using System.Threading.Tasks;
using GoSportBackEnd.Services.Models.Tennis;

namespace GoSportBackEnd.Services.Gateways.Interfaces
{
    public interface ITennisGameGateway
    {
        Task<MatchDetails> GetAsync(string id);
        Task<MatchDetails> UpdateAsync(MatchDetails matchDetails);
    }
}
