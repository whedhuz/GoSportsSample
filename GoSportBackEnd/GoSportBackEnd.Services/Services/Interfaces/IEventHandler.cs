using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;

namespace GoSportBackEnd.Services.Services.Interfaces
{
    public interface IEventHandler
    {
        Task<EventResponse> ProcessEventAsync(Event eventObj);
    }
}
