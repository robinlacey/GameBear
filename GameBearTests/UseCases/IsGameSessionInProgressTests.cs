using GameBear.Exceptions;
using GameBearTests.Mocks;
using GameBear.UseCases.RequestGameCheckExistingSession;
using Messages;
using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class IsGameSessionInProgressTests
    {
        public class GivenInvalidInput
        {
            public class WhenSessionIDIsInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidSessionID(string invalidInput)
                {
                    Assert.Throws<InvalidSessionIDException>(() =>
                        new IsGameSessionInProgress(new GameDataGatewayDummy(), new PublishEndPointDummy()).Execute(invalidInput, "Scout"));
                }
            }

            public class WhenMessageIDIsInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidMessageID(string invalidInput)
                {
                    Assert.Throws<InvalidMessageIDException>(() =>
                        new IsGameSessionInProgress(new GameDataGatewayDummy(), new PublishEndPointDummy()).Execute(
                            "Scout Is Valid", invalidInput));
                }
            }
        }

        public class GivenValidInput
        {
            public class WhenGameSessionGatewayIsCalled
            {
                [TestCase("Scout The Dog")]
                [TestCase("Is A Good Dog")]
                public void ThenGameSessionGatewayIsExistingSessionIsCalled(string sessionID)
                {
                    GameDataGatewaySpy spy = new GameDataGatewaySpy();
                    new IsGameSessionInProgress(spy,
                        new PublishEndPointDummy()).Execute(sessionID, "Scout" );
                    Assert.True(spy.IsExistingSessionSessionID == sessionID);
                }
            }

            public class WhenGameSessionIsFound
            {
                [TestCase("Scout")]
                [TestCase("Sit")]
                public void ThenSessionIDIsPublishedToSessionFoundQueue(string sessionID)
                {
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, true);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    new IsGameSessionInProgress(stub, spy).Execute(sessionID, "Hello Scout");
                    Assert.True(spy.MessageObject is IRequestGameSessionFound);
                    IRequestGameSessionFound messageObject = spy.MessageObject as IRequestGameSessionFound;
                    Assert.True(messageObject.SessionID == sessionID);
                }

                [TestCase("Scout")]
                [TestCase("Stay")]
                public void ThenMessageIDIsPublishedToSessionFoundQueue(string messageID)
                {
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, true);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    new IsGameSessionInProgress(stub, spy).Execute("Scout Is A Good Dog", messageID);
                    Assert.True(spy.MessageObject is IRequestGameSessionFound);
                    IRequestGameSessionFound messageObject = spy.MessageObject as IRequestGameSessionFound;
                    Assert.True(messageObject.MessageID == messageID);
                }
            }

            public class WhenGameSessionIsNotFound
            {
                [TestCase("Scout The Dog")]
                [TestCase("Doesn't Like Cats")]
                public void ThenSessionIDIsPublishedToSessionNotFoundQueue(string sessionID)
                {
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    new IsGameSessionInProgress(stub,spy).Execute(sessionID, "Wag");
                    Assert.True(spy.MessageObject is IRequestGameSessionNotFound);
                    IRequestGameSessionNotFound messageObject = spy.MessageObject as IRequestGameSessionNotFound;
                    Assert.True(messageObject.SessionID == sessionID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Likes Shoes")]
                public void ThenMessageIDIsPublishedToSessionNotFoundQueue(string messageID)
                {
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    new IsGameSessionInProgress(stub, spy).Execute("Scout Is A Good Dog", messageID);
                    Assert.True(spy.MessageObject is IRequestGameSessionNotFound);
                    IRequestGameSessionNotFound messageObject = spy.MessageObject as IRequestGameSessionNotFound;
                    Assert.True(messageObject.MessageID == messageID);
                }
            }
        }
    }
}