using System;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Services.Interfaces;

namespace GoSportBackEnd.Services.Services
{
    public class EventHandler : IEventHandler
    {
        public Task ProcessEventAsync(Event eventObj)
        {
            throw new NotImplementedException();
        }
    }
}
