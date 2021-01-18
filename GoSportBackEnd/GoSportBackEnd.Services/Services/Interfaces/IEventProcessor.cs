using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;

namespace GoSportBackEnd.Services.Services.Interfaces
{
    public interface IEventProcessor
    {
        bool CanProcess(string eventType);
        Task<EventResponse> ProcessEventAsync(Event eventObj);
    }
}
