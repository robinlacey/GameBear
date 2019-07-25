using DealerBear.Adaptor.Interface;
using GameBear.Data;
using GameBear.Exceptions;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBear.UseCases.SaveGameData
{
    public class SaveNewGameData:ISaveNewGameData
    {
        public void Execute(string sessionID, string messageID, IGameData gameData, IGameDataGateway gameDataGateway, IPublishMessageAdaptor publishMessageAdaptor)
        {
            if (InvalidIDString(sessionID))
            {
                throw new InvalidSessionIDException();
            }
            if (InvalidIDString(messageID))
            {
                throw new InvalidMessageIDException();
            }
            if (InvalidIDString(gameData.CurrentCardID))
            {
                throw new InvalidCardIDException();
            }
            gameDataGateway.Save(sessionID,gameData);
            publishMessageAdaptor.Publish(new GameResponse()
            {
                SessionID = sessionID,
                MessageID = messageID,
                CardsToAdd = gameData.CardsToAdd,
                CurrentCardID = gameData.CurrentCardID,
                Seed = gameData.Seed,
                PackVersion = gameData.PackVersion
            });

        }
        
        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}