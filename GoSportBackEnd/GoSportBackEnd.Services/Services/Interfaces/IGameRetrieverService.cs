using System.Threading.Tasks;
using GoSportBackEnd.Services.Models.Tennis;

namespace GoSportBackEnd.Services.Services.Interfaces
{
    public interface IGameRetrieverService
    {
        Task<MatchDetails> GetTennisMatchDetails(string id);
    }
}
