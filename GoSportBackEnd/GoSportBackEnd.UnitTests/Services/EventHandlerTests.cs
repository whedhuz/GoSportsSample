using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Models.Tennis;
using GoSportBackEnd.Services.Services;
using GoSportBackEnd.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace GoSportBackEnd.UnitTests.Services
{
    public class EventHandlerTests
    {
        private EventHandler _sut;
        private Mock<IEventProcessor> _eventProcessorMock;
        private Event _eventobj;

        [SetUp]
        public virtual void Setup()
        {
            _eventProcessorMock = new Mock<IEventProcessor>();

            _sut = new EventHandler(Mock.Of<ILogger<EventHandler>>(), new List<IEventProcessor>
            {
                _eventProcessorMock.Object
            });
        }

        public class WhenEventTypeCanBeProcessed : EventHandlerTests
        {
            public override void Setup()
            {
                base.Setup();
                _eventobj = new Event
                {
                    Type = "able.to.process",
                    ContentJson = JsonSerializer.Serialize(new TennisEventObject
                    {
                        GameId = "gameId"
                    })
                };
                
                _eventProcessorMock.Setup(m => m.CanProcess("able.to.process")).Returns(true);
            }

            [Test]
            public async Task ThenEventProcessorRuns()
            {
                await _sut.ProcessEventAsync(_eventobj);
                
                _eventProcessorMock.Verify(m => m.CanProcess("able.to.process"), Times.Once);
                _eventProcessorMock.Verify(m => m.ProcessEventAsync(It.IsAny<Event>()), Times.Once);
            }
        }

        public class WhenEventTypeCannotBeProcessed : EventHandlerTests
        {
            public override void Setup()
            {
                base.Setup();
                _eventobj = new Event
                {
                    Type = "able.to.process",
                    ContentJson = JsonSerializer.Serialize(new TennisEventObject
                    {
                        GameId = "gameId"
                    })
                };
                
                _eventProcessorMock.Setup(m => m.CanProcess("able.to.process")).Returns(false);
            }

            [Test]
            public async Task ThenReturnsErrorResponse()
            {
                var response = await _sut.ProcessEventAsync(_eventobj);

                Assert.IsAssignableFrom<ErrorResponse>(response);
                _eventProcessorMock.Verify(m => m.CanProcess("able.to.process"), Times.Once);
                _eventProcessorMock.Verify(m => m.ProcessEventAsync(It.IsAny<Event>()), Times.Never);
            }
        }
    }
}
