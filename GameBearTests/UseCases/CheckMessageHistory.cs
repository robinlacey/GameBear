using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveNewGameData;
using GameBearTests.Mocks;
using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class CheckMessageHistory
    {
        public class GivenExistingSessionID
        {
            public class WhenMessageIDIsInHistory
            {
                [TestCase("Scout")]
                [TestCase("Did Not Run Away")]
                [TestCase("Good Dog")]
                public void ThenWillGetMessageIDHistoryFromGateway(string sessionID)
                {
                    GameBear.UseCases.SaveNewGameData.CheckMessageHistory newGameData =
                        new GameBear.UseCases.SaveNewGameData.CheckMessageHistory();
                    GameDataGatewayStub gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                    SessionIDMessageHistoryGatewaySpy spy = new SessionIDMessageHistoryGatewaySpy();
                    newGameData.Execute(sessionID, "MessageID", 1, 1, "CardID", new SaveGameDataDummy(), spy,
                        gameDataGatewayStub);
                    Assert.True(spy.GetMessageIDHistoryCalled);
                    Assert.True(spy.GetMessageIDHistoryCalledSessionID == sessionID);
                }

                [TestCase("Scout")]
                [TestCase("The")]
                [TestCase("Dog")]
                public void ThenDataIsNotSaved(string messageID)
                {
                    SaveGameDataSpy spy = new SaveGameDataSpy();
                    GameBear.UseCases.SaveNewGameData.CheckMessageHistory newGameData =
                        new GameBear.UseCases.SaveNewGameData.CheckMessageHistory();
                    GameDataGatewayStub gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                    SessionIDMessageHistoryGatewayStub stub =
                        new SessionIDMessageHistoryGatewayStub(new[] {"Scout", messageID, "Dog"});
                    newGameData.Execute("SessionID", messageID, 1, 1, "CardID", spy, stub, gameDataGatewayStub);
                    Assert.False(spy.ExecuteCalled);
                }
            }

            public class WhenMessageIDIsNotInHistory
            {
                [TestCase("Scout")]
                [TestCase("Did Not Run Away")]
                [TestCase("Good Dog")]
                public void ThenWillGetMessageIDHistoryFromGateway(string sessionID)
                {
                    GameBear.UseCases.SaveNewGameData.CheckMessageHistory newGameData =
                        new GameBear.UseCases.SaveNewGameData.CheckMessageHistory();
                    IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                    SessionIDMessageHistoryGatewaySpy spy = new SessionIDMessageHistoryGatewaySpy();
                    newGameData.Execute(sessionID, "MessageID", 1, 1, "CardID", new SaveGameDataDummy(), spy,
                        gameDataGatewayStub);
                    Assert.True(spy.GetMessageIDHistoryCalled);
                    Assert.True(spy.GetMessageIDHistoryCalledSessionID == sessionID);
                }

                [TestCase("Woof")]
                [TestCase("ArrWowoWooo!")]
                public void ThenWillStoreMessageIDInMessageIDHistory(string messageID)
                {
                    GameBear.UseCases.SaveNewGameData.CheckMessageHistory newGameData =
                        new GameBear.UseCases.SaveNewGameData.CheckMessageHistory();
                    IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                    SaveGameDataSpy spy = new SaveGameDataSpy();
                    newGameData.Execute("SessionID", messageID, 1, 1, "CardID", spy,
                        new SessionIDMessageHistoryGatewayStub(new string[0]), gameDataGatewayStub);
                    Assert.True(spy.ExecuteCalled);
                }

                [TestCase("Scout", "The", 10, 0, "Percent Doggo")]
                [TestCase("Scout", "The", 0, 0, "Percent Catto")]
                public void ThenDataIsSaved(string sessionID, string messageID, int seed, int version,
                    string currentCard)
                {
                    GameBear.UseCases.SaveNewGameData.CheckMessageHistory newGameData =
                        new GameBear.UseCases.SaveNewGameData.CheckMessageHistory();
                    IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                    SaveGameDataSpy saveGameDataSpy = new SaveGameDataSpy();
                    newGameData.Execute(sessionID, messageID, seed, version, currentCard, saveGameDataSpy,
                        new SessionIDMessageHistoryGatewayStub(new string[0]), gameDataGatewayStub);
                    Assert.True(saveGameDataSpy.ExecuteCalled);
                    Assert.True(saveGameDataSpy.SessionID == sessionID);
                    Assert.True(saveGameDataSpy.GameDataToSave.Seed == seed);
                    Assert.True(saveGameDataSpy.GameDataToSave.PackVersion == version);
                    Assert.True(saveGameDataSpy.GameDataToSave.CurrentCardID == currentCard);
                }
            }
        }

        public class GivenNoExistingSessionID
        {
            [Test]
            public void ThenNotWillStoreMessageIDInMessageIDHistory()
            {
                GameBear.UseCases.SaveNewGameData.CheckMessageHistory newGameData =
                    new GameBear.UseCases.SaveNewGameData.CheckMessageHistory();
                IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), false);
                SessionIDMessageHistoryGatewaySpy spy = new SessionIDMessageHistoryGatewaySpy();
                newGameData.Execute("SessionID", "MessageID", 1, 1, "CardID", new SaveGameDataDummy(), spy,
                    gameDataGatewayStub);
                Assert.False(spy.GetMessageIDHistoryCalled);
            }

            [Test]
            public void ThenDataIsNotSaved()
            {
                GameBear.UseCases.SaveNewGameData.CheckMessageHistory newGameData =
                    new GameBear.UseCases.SaveNewGameData.CheckMessageHistory();
                IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), false);
                SaveGameDataSpy saveGameDataSpy = new SaveGameDataSpy();
                newGameData.Execute("SessionID", "MessageID", 1, 1, "CardID", saveGameDataSpy,
                    new SessionIDMessageHistoryGatewayStub(new string[0]), gameDataGatewayStub);
                Assert.False(saveGameDataSpy.ExecuteCalled);
            }
        }
    }
}