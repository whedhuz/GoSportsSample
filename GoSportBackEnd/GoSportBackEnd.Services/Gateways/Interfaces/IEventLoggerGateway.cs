using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;

namespace GoSportBackEnd.Services.Gateways.Interfaces
{
    public interface IEventLoggerGateway
    {
        Task LogEvent(Event eventObj, bool processSuccessful);
    }
}
