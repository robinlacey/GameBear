using System;
using System.Collections.Generic;
using DealerBear.Messages;
using GameBear.Exceptions;
using GameBear.UseCases.SaveGameData;
using GameBearTests.Mocks;
using NUnit.Framework;

namespace GameBearTests.UseCases
{
    public class SaveNewGameDataTests
    {
        public class GivenInvalidInput
        {
            public class WhenSessionIDIsInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidSessionIDException(string sessionID)
                {
                    SaveNewGameData saveNewGameData =
                        new SaveNewGameData();
                    Assert.Throws<InvalidSessionIDException>(() => {  saveNewGameData.Execute(sessionID, "MessageID", new GameDataDummy(), new GameDataGatewayDummy(), new PublishEndPointDummy());});
                }
                
            }
            
            public class WhenMessageIDIsInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidMessageIDException(string messageID)
                {
                    SaveNewGameData saveNewGameData =
                        new SaveNewGameData();
                    Assert.Throws<InvalidMessageIDException>(() => {  saveNewGameData.Execute("SessionID", messageID, new GameDataDummy(){CurrentCardID = "CardID"}, new GameDataGatewayDummy(), new PublishEndPointDummy());});
                }
            }
            public class WhenCardIDIsInvalid
            {
                [TestCase("    ")]
                [TestCase("")]
                [TestCase(null)]
                public void ThenThrowsInvalidCardIDException(string cardID)
                {
                    SaveNewGameData saveNewGameData =
                        new SaveNewGameData();
                    Assert.Throws<InvalidCardIDException>(() => {  saveNewGameData.Execute("SessionID", "MessageID", new GameDataDummy(){CurrentCardID = cardID}, new GameDataGatewayDummy(), new PublishEndPointDummy() );});
                }
            }
        }

        public class GivenValidInput
        {
            [TestCase("WOOF","Hello", "Scout",1,2,"The",0.5f)]
            [TestCase("MEOW","Wag", "Dog",-1,99,"Hello",1f)]
            public void ThenGameDataIsPassedToGateway(string sessionID, string messageID, string cardID, int packVersion, int seed, string cardtoAdd, float probability)
            {
                GameDataGatewaySpy gameDataGatewaySpy = new GameDataGatewaySpy();
                Tuple<string, float>[] cardsToAdd = {new Tuple<string, float>(cardtoAdd, probability)};
                SaveNewGameData saveNewGameData =
                    new SaveNewGameData();
                saveNewGameData.Execute(
                    sessionID,
                    messageID,
                    
                    new GameDataDummy()
                    {
                        CurrentCardID = cardID,
                        Seed = seed,
                        PackVersion = packVersion,
                        CardsToAdd = cardsToAdd
                    },
                    gameDataGatewaySpy, 
                    new PublishEndPointDummy());
                Assert.True(gameDataGatewaySpy.SaveSessionID == sessionID);
                Assert.True(gameDataGatewaySpy.SaveGameData.CurrentCardID == cardID);
                Assert.True(gameDataGatewaySpy.SaveGameData.Seed == seed);
                Assert.True(gameDataGatewaySpy.SaveGameData.PackVersion == packVersion);
                Assert.True(gameDataGatewaySpy.SaveGameData.CardsToAdd[0].Item1 == cardtoAdd);
                Assert.True(Math.Abs(gameDataGatewaySpy.SaveGameData.CardsToAdd[0].Item2 - probability) < 0.05f);
            }
            [TestCase("WOOF","Hello", "Scout",1,2,"The",0.5f, "Dog",1999)]
            [TestCase("MEOW","Wag", "Dog",-1,99,"Hello",1f,"Doggo", 1982)]
            public void ThenGameResponseIsPublishedWithCorrectGameData(string sessionID, string messageID, string cardID, int packVersion, int seed, string cardtoAdd, float probability, string statKey, int statValue)
            {
                GameDataGatewaySpy gameDataGatewaySpy = new GameDataGatewaySpy();
                PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                Tuple<string, float>[] cardsToAdd = {new Tuple<string, float>(cardtoAdd, probability)};
                SaveNewGameData saveNewGameData =
                    new SaveNewGameData();
                saveNewGameData.Execute(
                    sessionID,
                    messageID,
                    new GameDataDummy
                    {
                        CurrentCardID = cardID,
                        Seed = seed,
                        PackVersion = packVersion,
                        CardsToAdd = cardsToAdd,
                        CurrentStats = new Dictionary<string, int>{{statKey,statValue}}
                    },
                    gameDataGatewaySpy, 
                    publishEndPointSpy);
                Assert.True(publishEndPointSpy.MessageObject is IGameResponse);
                IGameResponse gameResponse = (IGameResponse) publishEndPointSpy.MessageObject;
                Assert.True(gameResponse.Seed == seed);
                Assert.True(gameResponse.PackVersion == packVersion);
                Assert.True(gameResponse.CurrentCardID == cardID);
                Assert.True(gameResponse.CardsToAdd[0].Item1 == cardtoAdd);
                Assert.True(gameResponse.CardsToAdd[0].Item2 == probability);
                Assert.True(gameResponse.Seed == seed);
                Assert.True(gameResponse.CurrentStats.ContainsKey(statKey));
                Assert.True(gameResponse.CurrentStats[statKey] == statValue);
            }
            [TestCase("WOOF","Hello", "Scout",1,2,"The",0.5f)]
            [TestCase("MEOW","Wag", "Dog",-1,99,"Hello",1f)]
            public void ThenGameResponseIsPublishedWithMessageIDAndSessionID(string sessionID, string messageID, string cardID, int packVersion, int seed, string cardtoAdd, float probability)
            {
                GameDataGatewaySpy gameDataGatewaySpy = new GameDataGatewaySpy();
                PublishEndPointSpy publishEndPointSpy = new PublishEndPointSpy();
                Tuple<string, float>[] cardsToAdd = {new Tuple<string, float>(cardtoAdd, probability)};
                SaveNewGameData saveNewGameData =
                    new SaveNewGameData();
                saveNewGameData.Execute(
                    sessionID,
                    messageID,
                    new GameDataDummy
                    {
                        CurrentCardID = cardID,
                        Seed = seed,
                        PackVersion = packVersion,
                        CardsToAdd = cardsToAdd
                    },
                    gameDataGatewaySpy, 
                    publishEndPointSpy);
                Assert.True(publishEndPointSpy.MessageObject is IGameResponse);
                IGameResponse gameResponse = (IGameResponse) publishEndPointSpy.MessageObject;
                Assert.True(gameResponse.SessionID == sessionID);
                Assert.True(gameResponse.MessageID == messageID);
            }
        }
    }
}