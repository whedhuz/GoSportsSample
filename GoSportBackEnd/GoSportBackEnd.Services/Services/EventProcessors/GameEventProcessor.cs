using System;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Services.Interfaces;

namespace GoSportBackEnd.Services.Services.EventProcessors
{
    public class GameEventProcessor : IEventProcessor
    {
        public bool CanProcess(string eventType)
        {
            throw new NotImplementedException();
        }

        public Task ProcessEvent(Event eventObj)
        {
            throw new NotImplementedException();
        }
    }
}
