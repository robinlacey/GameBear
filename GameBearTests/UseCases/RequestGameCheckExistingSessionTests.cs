using GameBear.Exceptions;
using GameBearTests.Mocks;
using GameBear.UseCases.RequestGameCheckExistingSession;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
using Messages;
using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class RequestGameCheckExistingSessionTests
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
                    IRequestGameCheckExistingSession requestGameCheckExistingSession =
                        new RequestGameCheckExistingSession();
                    Assert.Throws<InvalidSessionIDException>(() =>
                        requestGameCheckExistingSession.Execute(
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
                    IRequestGameCheckExistingSession requestGameCheckExistingSession =
                        new RequestGameCheckExistingSession();
                    Assert.Throws<InvalidMessageIDException>(() =>
                        requestGameCheckExistingSession.Execute(
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
                    IRequestGameCheckExistingSession requestGameCheckExistingSession =
                        new RequestGameCheckExistingSession();
                    GameDataGatewaySpy spy = new GameDataGatewaySpy();
                    requestGameCheckExistingSession.Execute(new RequestGameIsSessionIDInUseStub(sessionID, "Scout"),
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
                    IRequestGameCheckExistingSession requestGameCheckExistingSession =
                        new RequestGameCheckExistingSession();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, true);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    requestGameCheckExistingSession.Execute(
                        new RequestGameIsSessionIDInUseStub(sessionID, "Hello Scout"), stub, spy);
                    Assert.True(spy.MessageObject is IRequestGameSessionFound);
                    IRequestGameSessionFound messageObject = spy.MessageObject as IRequestGameSessionFound;
                    Assert.True(messageObject.SessionID == sessionID);
                }

                [TestCase("Scout")]
                [TestCase("Stay")]
                public void ThenMessageIDIsPublishedToSessionFoundQueue(string messageID)
                {
                    IRequestGameCheckExistingSession requestGameCheckExistingSession =
                        new RequestGameCheckExistingSession();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, true);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    requestGameCheckExistingSession.Execute(
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
                    IRequestGameCheckExistingSession requestGameCheckExistingSession =
                        new RequestGameCheckExistingSession();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    requestGameCheckExistingSession.Execute(new RequestGameIsSessionIDInUseStub(sessionID, "Wag"), stub,
                        spy);
                    Assert.True(spy.MessageObject is IRequestGameSessionNotFound);
                    IRequestGameSessionNotFound messageObject = spy.MessageObject as IRequestGameSessionNotFound;
                    Assert.True(messageObject.SessionID == sessionID);
                }

                [TestCase("Scout The Dog")]
                [TestCase("Likes Shoes")]
                public void ThenMessageIDIsPublishedToSessionNotFoundQueue(string messageID)
                {
                    IRequestGameCheckExistingSession requestGameCheckExistingSession =
                        new RequestGameCheckExistingSession();
                    GameDataGatewayStub stub = new GameDataGatewayStub(null, false);
                    PublishEndPointSpy spy = new PublishEndPointSpy();
                    requestGameCheckExistingSession.Execute(
                        new RequestGameIsSessionIDInUseStub("Scout Is A Good Dog", messageID), stub, spy);
                    Assert.True(spy.MessageObject is IRequestGameSessionNotFound);
                    IRequestGameSessionNotFound messageObject = spy.MessageObject as IRequestGameSessionNotFound;
                    Assert.True(messageObject.MessageID == messageID);
                }
            }
        }
    }
}