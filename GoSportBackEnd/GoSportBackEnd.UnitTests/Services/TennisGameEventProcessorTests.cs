using System.Threading.Tasks;
using GoSportBackEnd.Services.Gateways.Interfaces;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Services.EventProcessors;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace GoSportBackEnd.UnitTests.Services
{
    public class TennisGameEventProcessorTests
    {
        private TennisGameEventProcessor _sut;
        private Mock<IEventLoggerGateway> _eventLoggerGatewayMock;
        private Event _eventobj;

        [SetUp]
        public virtual void Setup()
        {
            _eventLoggerGatewayMock = new Mock<IEventLoggerGateway>();
            _sut = new TennisGameEventProcessor(Mock.Of<ILogger<TennisGameEventProcessor>>(), _eventLoggerGatewayMock.Object);
        }

        public class WhenTennisGameChangeServerEvent : TennisGameEventProcessorTests
        {
            public override void Setup()
            {
                base.Setup();
                _eventobj = new Event
                {
                    Type = "game.tennis.changeserver",
                    Content = new
                    {
                        gameId = "gameId"
                    }
                };
            }

            [Test]
            public async Task ThenUpdateEventLogDetails()
            {
                var response = await _sut.ProcessEventAsync(_eventobj);
                _eventLoggerGatewayMock.Verify(m => m.LogEvent(It.IsAny<Event>()), Times.Once);
            }
        }
    }
}
