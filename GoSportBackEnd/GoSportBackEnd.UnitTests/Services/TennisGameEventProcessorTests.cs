using System;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Gateways.Interfaces;
using GoSportBackEnd.Services.Models;
using GoSportBackEnd.Services.Models.Tennis;
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
        private Mock<ITennisGameGateway> _tennisGameGateway;
        private Event _eventobj;

        [SetUp]
        public virtual void Setup()
        {
            _eventLoggerGatewayMock = new Mock<IEventLoggerGateway>();
            _tennisGameGateway = new Mock<ITennisGameGateway>();
            _sut = new TennisGameEventProcessor(Mock.Of<ILogger<TennisGameEventProcessor>>(),
                _eventLoggerGatewayMock.Object, _tennisGameGateway.Object);
        }

        public class WhenExceptionOccur : TennisGameEventProcessorTests
        {
            public override void Setup()
            {
                base.Setup();
                _eventobj = new Event
                {
                    Type = "game.tennis.unknown",
                    Content = "gameId"
                };
            }

            [Test]
            public void ThenUpdateEventLogDetails_WithFailureToProcess()
            {
                Assert.ThrowsAsync<ApplicationException>(async () => await _sut.ProcessEventAsync(_eventobj));
                _eventLoggerGatewayMock.Verify(m => m.LogEvent(It.IsAny<Event>(), It.Is<bool>(b => b == false)),
                    Times.Once);
            }
        }

        public class WhenTennisGameChangeServerEvent : TennisGameEventProcessorTests
        {
            private MatchDetails _tennisMatchDetails;

            public override void Setup()
            {
                base.Setup();
                _eventobj = new Event
                {
                    Type = "game.tennis.changeserver",
                    Content = "gameId"
                };

                _tennisMatchDetails = new MatchDetails
                {
                    Id = "gameId",
                    ServingPlayer = 1
                };
                _tennisGameGateway.Setup(m => m.GetAsync(_tennisMatchDetails.Id)).ReturnsAsync(_tennisMatchDetails);
                _tennisGameGateway.Setup(m => m.UpdateAsync(It.IsAny<MatchDetails>()))
                    .Callback<MatchDetails>(input => _tennisMatchDetails = input)
                    .ReturnsAsync(_tennisMatchDetails);
            }

            [Test]
            public async Task AndPlayer1IsServing_ThenReturnsSuccessResponse_WithPlayer2Serving()
            {
                _tennisMatchDetails.ServingPlayer = 1;

                var response = await _sut.ProcessEventAsync(_eventobj);
                _eventLoggerGatewayMock.Verify(m => m.LogEvent(It.IsAny<Event>(), It.Is<bool>(b => b == true)),
                    Times.Once);

                Assert.IsAssignableFrom<SuccessResponse>(response);
                var returnedMatchDetails = ((SuccessResponse) response).ResponseObj as MatchDetails;
                Assert.AreEqual(2, returnedMatchDetails.ServingPlayer);
            }

            [Test]
            public async Task AndPlayer2IsServing_ThenReturnsSuccessResponse_WithPlayer1Serving()
            {
                _tennisMatchDetails.ServingPlayer = 2;

                var response = await _sut.ProcessEventAsync(_eventobj);
                _eventLoggerGatewayMock.Verify(m => m.LogEvent(It.IsAny<Event>(), It.Is<bool>(b => b == true)),
                    Times.Once);

                Assert.IsAssignableFrom<SuccessResponse>(response);
                var returnedMatchDetails = ((SuccessResponse) response).ResponseObj as MatchDetails;
                Assert.AreEqual(1, returnedMatchDetails.ServingPlayer);
            }

            [Test]
            public async Task ThenUpdateEventLogDetails_WithSuccessToProcess()
            {
                var response = await _sut.ProcessEventAsync(_eventobj);
                _eventLoggerGatewayMock.Verify(m => m.LogEvent(It.IsAny<Event>(), It.Is<bool>(b => b == true)),
                    Times.Once);
            }
        }
    }
}
