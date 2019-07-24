using GameBear.Exceptions;
using GameBearTests.Mocks;
using GameBear.UseCases.RequestGameCheckExistingSession;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
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
                    IIsGameSessionInProgress isGameSessionInProgress =
                        new IsGameSessionInProgress();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        isGameSessionInProgress.Execute(
                            new RequestGameIsSessionIDInUseStub(invalidInput, "Scout"),
                            new GameDataGatewayDummy(), new PublishEndPointDummy()));
                }
            }

            public class WhenMessageIDIsInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidMessageID(string invalidInput)
                {
                    IIsGameSessionInProgress isGameSessionInProgress =
                        new IsGameSessionInProgress();
                    Assert.Throws<InvalidMessageIDException>(() =>
                        isGameSessionInProgress.Execute(
                            new RequestGameIsSessionIDInUseStub("Scout Is Valid", invalidInput),
                            new GameDataGatewayDummy(), new PublishEndPointDummy()));
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
                    IIsGameSessionInProgress isGameSessionInProgress =
                        new IsGameSessionInProgress();
                    GameDataGatewaySpy spy = new GameDataGatewaySpy();
                    isGameSessionInProgress.Execute(new RequestGameIsSessionIDInUseStub(sessionID, "Scout"),
                        spy,
                        new PublishEndPointDummy());
                    Assert.True(spy.IsExistingSessionSessionID == sessionID);
                }
            }

            public class WhenGameSessionIsFound
            {
                [TestCase("Scout")]
                [TestCase("Sit")]
                public void ThenSessionIDIsPublishedToSessionFoundQueue(string sessionID)
                {
                    IIsGameSessionInProgress isGameSessionInProgress =
                        new IsGameSessionInProgress();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, true);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    isGameSessionInProgress.Execute(
                        new RequestGameIsSessionIDInUseStub(sessionID, "Hello Scout"), stub, spy);
                    Assert.True(spy.MessageObject is IRequestGameSessionFound);
                    IRequestGameSessionFound messageObject = spy.MessageObject as IRequestGameSessionFound;
                    Assert.True(messageObject.SessionID == sessionID);
                }

                [TestCase("Scout")]
                [TestCase("Stay")]
                public void ThenMessageIDIsPublishedToSessionFoundQueue(string messageID)
                {
                    IIsGameSessionInProgress isGameSessionInProgress =
                        new IsGameSessionInProgress();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, true);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    isGameSessionInProgress.Execute(
                        new RequestGameIsSessionIDInUseStub("Scout Is A Good Dog", messageID), stub, spy);
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
                    IIsGameSessionInProgress isGameSessionInProgress =
                        new IsGameSessionInProgress();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    isGameSessionInProgress.Execute(new RequestGameIsSessionIDInUseStub(sessionID, "Wag"), stub,
                        spy);
                    Assert.True(spy.MessageObject is IRequestGameSessionNotFound);
                    IRequestGameSessionNotFound messageObject = spy.MessageObject as IRequestGameSessionNotFound;
                    Assert.True(messageObject.SessionID == sessionID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Likes Shoes")]
                public void ThenMessageIDIsPublishedToSessionNotFoundQueue(string messageID)
                {
                    IIsGameSessionInProgress isGameSessionInProgress =
                        new IsGameSessionInProgress();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    isGameSessionInProgress.Execute(
                        new RequestGameIsSessionIDInUseStub("Scout Is A Good Dog", messageID), stub, spy);
                    Assert.True(spy.MessageObject is IRequestGameSessionNotFound);
                    IRequestGameSessionNotFound messageObject = spy.MessageObject as IRequestGameSessionNotFound;
                    Assert.True(messageObject.MessageID == messageID);
                }
            }
        }
    }
}