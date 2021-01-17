using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;

namespace GoSportBackEnd.Services.Services.Interfaces
{
    public interface IEventHandler
    {
        Task ProcessEventAsync(Event eventObj);
    }
}
