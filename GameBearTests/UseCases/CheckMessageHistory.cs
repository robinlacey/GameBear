using System.Collections.Generic;
using GameBear.Exceptions;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveNewGameData;
using GameBearTests.Mocks;
using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class CheckMessageHistoryTests
    {
        public class GivenInvalidInput
        {
            public class WhenStartingStatsAreInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                public void ThenThrowsInvalidStartingStatsException(string invalidKey)
                {
                    CheckMessageHistory checkMessageHistoryUseCase =
                        new CheckMessageHistory();
                    Assert.Throws<InvalidStartingStatsException>(() =>
                    {
                        checkMessageHistoryUseCase.Execute(
                            "SessionID",
                            "MessageID", 
                            1,
                            2,
                            "Current Card",  
                            new Dictionary<string, int> {{invalidKey,1}}, 
                            new SaveNewGameDataDummy()
                            , new SessionIDMessageHistoryGatewaySpy(),
                            new GameDataGatewayDummy(),
                            new PublishEndPointDummy());
                    });
                    Assert.Throws<InvalidStartingStatsException>(() =>
                    {
                        checkMessageHistoryUseCase.Execute(
                            "SessionID",
                            "MessageID", 
                            1,
                            2,
                            "Current Card",  
                            null,
                            new SaveNewGameDataDummy()
                            , new SessionIDMessageHistoryGatewaySpy(),
                            new GameDataGatewayDummy(),
                            new PublishEndPointDummy());
                    });
                    Assert.Throws<InvalidStartingStatsException>(() =>
                    {
                        checkMessageHistoryUseCase.Execute(
                            "SessionID",
                            "MessageID",
                            1,
                            2,
                            "Current Card",
                            new Dictionary<string, int>(),
                            new SaveNewGameDataDummy()
                            , new SessionIDMessageHistoryGatewaySpy(),
                            new GameDataGatewayDummy(),
                            new PublishEndPointDummy());
                    });
                    
                }
            }
            public class WhenSessionIDIsInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidSessionIDException(string sessionID)
                {
                    CheckMessageHistory checkMessageHistoryUseCase =
                        new CheckMessageHistory();
                    Assert.Throws<InvalidSessionIDException>(() =>
                    {
                        checkMessageHistoryUseCase.Execute(
                            sessionID,
                            "MessageID",
                            1,
                            2,
                            "Current Card",
                            new Dictionary<string, int> {{"Dog",1}}, 
                            new SaveNewGameDataDummy(),
                            new SessionIDMessageHistoryGatewaySpy(),
                            new GameDataGatewayDummy(),
                            new PublishEndPointDummy());
                    });
                }
            }

            public class WhenMessageIDIsInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidSessionIDException(string messageID)
                {
                    CheckMessageHistory checkMessageHistoryUseCase =
                        new CheckMessageHistory();
                    Assert.Throws<InvalidMessageIDException>(() =>
                    {
                        checkMessageHistoryUseCase.Execute(
                            "SessionID",
                            messageID, 1,
                            2,
                            "Current Card",  
                            new Dictionary<string, int> {{"Dog",1}}, 
                            new SaveNewGameDataDummy()
                            , new SessionIDMessageHistoryGatewaySpy(),
                            new GameDataGatewayDummy(),
                            new PublishEndPointDummy());
                    });
                }
            }
        }

        public class GivenValidInput
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
                        CheckMessageHistory checkMessageHistoryUseCase =
                            new CheckMessageHistory();
                        GameDataGatewayStub gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                        SessionIDMessageHistoryGatewaySpy spy = new SessionIDMessageHistoryGatewaySpy();
                        checkMessageHistoryUseCase.Execute(
                            sessionID,
                            "MessageID",
                            1,
                            1,
                            "CardID",
                            new Dictionary<string, int> {{"Dog",1}}, 
                            new SaveNewGameDataDummy(),
                            spy,
                            gameDataGatewayStub,
                            new PublishEndPointDummy());
                        Assert.True(spy.GetMessageIDHistoryCalled);
                        Assert.True(spy.GetMessageIDHistoryCalledSessionID == sessionID);
                    }

                    [TestCase("Scout")]
                    [TestCase("The")]
                    [TestCase("Dog")]
                    public void ThenDataIsNotSaved(string messageID)
                    {
                        SaveNewGameDataSpy spy = new SaveNewGameDataSpy();
                        CheckMessageHistory checkMessageHistoryUseCase = new CheckMessageHistory();
                        GameDataGatewayStub gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                        SessionIDMessageHistoryGatewayStub stub =
                            new SessionIDMessageHistoryGatewayStub(new[] {"Scout", messageID, "Dog"});
                        checkMessageHistoryUseCase.Execute(
                            "SessionID",
                            messageID,
                            1,
                            1,
                            "CardID",
                            new Dictionary<string, int> {{"Dog",1}}, 
                            spy,
                            stub,
                            gameDataGatewayStub,
                            new PublishEndPointDummy());
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
                        CheckMessageHistory checkMessageHistoryUseCase = new CheckMessageHistory();
                        IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                        SessionIDMessageHistoryGatewaySpy spy = new SessionIDMessageHistoryGatewaySpy();
                        checkMessageHistoryUseCase.Execute(sessionID, "MessageID", 1, 1, "CardID",  new Dictionary<string, int> {{"Dog",1}}, 
                            new SaveNewGameDataDummy(), spy,
                            gameDataGatewayStub, new PublishEndPointDummy());
                        Assert.True(spy.GetMessageIDHistoryCalled);
                        Assert.True(spy.GetMessageIDHistoryCalledSessionID == sessionID);
                    }

                    [TestCase("Woof")]
                    [TestCase("ArrWowoWooo!")]
                    public void ThenWillStoreMessageIDInMessageIDHistory(string messageID)
                    {
                        CheckMessageHistory checkMessageHistoryUseCase =
                            new CheckMessageHistory();
                        IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                        SaveNewGameDataSpy spy = new SaveNewGameDataSpy();
                        checkMessageHistoryUseCase.Execute("SessionID", messageID, 1, 1, "CardID",  new Dictionary<string, int> {{"Dog",1}},  spy,
                            new SessionIDMessageHistoryGatewayStub(new string[0]), gameDataGatewayStub,
                            new PublishEndPointDummy());
                        Assert.True(spy.ExecuteCalled);
                    }

                    [TestCase("Scout", "The", 10, 0, "Percent Doggo","Stat Dog", 99)]
                    [TestCase("Scout", "The", 0, 0, "Percent Catto","Yes Dog", -100)]
                    public void ThenDataIsSaved(string sessionID, string messageID, int seed, int version,
                        string currentCard, string statName, int statValue)
                    {
                        CheckMessageHistory checkMessageHistoryUseCase = new CheckMessageHistory();
                        IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), true);
                        SaveNewGameDataSpy saveNewGameDataSpy = new SaveNewGameDataSpy();
                        checkMessageHistoryUseCase.Execute(sessionID, messageID, seed, version, currentCard,  
                            new Dictionary<string, int> {{statName,statValue}},
                            saveNewGameDataSpy,
                            new SessionIDMessageHistoryGatewayStub(new string[0]), gameDataGatewayStub,
                            new PublishEndPointDummy());
                        Assert.True(saveNewGameDataSpy.ExecuteCalled);
                        Assert.True(saveNewGameDataSpy.SessionID == sessionID);
                        Assert.True(saveNewGameDataSpy.GameDataToSave.Seed == seed);
                        Assert.True(saveNewGameDataSpy.GameDataToSave.PackVersion == version);
                        Assert.True(saveNewGameDataSpy.GameDataToSave.CurrentCardID == currentCard);
                        Assert.True(saveNewGameDataSpy.GameDataToSave.CurrentStats.ContainsKey(statName));
                        Assert.True(saveNewGameDataSpy.GameDataToSave.CurrentStats[statName] == statValue);
                    }
                }
            }

            public class GivenNoExistingSessionID
            {
                [Test]
                public void ThenNotWillStoreMessageIDInMessageIDHistory()
                {
                    CheckMessageHistory checkMessageHistoryUseCase = new CheckMessageHistory();
                    IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), false);
                    SessionIDMessageHistoryGatewaySpy spy = new SessionIDMessageHistoryGatewaySpy();
                    checkMessageHistoryUseCase.Execute("SessionID", "MessageID", 1, 1, "CardID",  new Dictionary<string, int> {{"Dog",1}}, 
                        new SaveNewGameDataDummy(), spy,
                        gameDataGatewayStub, new PublishEndPointDummy());
                    Assert.False(spy.GetMessageIDHistoryCalled);
                }

                [Test]
                public void ThenDataIsNotSaved()
                {
                    CheckMessageHistory checkMessageHistoryUseCase = new CheckMessageHistory();
                    IGameDataGateway gameDataGatewayStub = new GameDataGatewayStub(new GameDataDummy(), false);
                    SaveNewGameDataSpy saveNewGameDataSpy = new SaveNewGameDataSpy();
                    checkMessageHistoryUseCase.Execute("SessionID", "MessageID", 1, 1, "CardID",   new Dictionary<string, int> {{"Dog",1}}, saveNewGameDataSpy,
                        new SessionIDMessageHistoryGatewayStub(new string[0]), gameDataGatewayStub,
                        new PublishEndPointDummy());
                    Assert.False(saveNewGameDataSpy.ExecuteCalled);
                }
            }
        }
    }
}