using DealerBear.Adaptor.Interface;
using GameBear.Data;
using GameBear.Exceptions;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBear.UseCases.SaveGameData
{
    public class SaveNewGameData:ISaveNewGameData
    {
        private readonly IGameDataGateway _gameDataGateway;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public SaveNewGameData(IGameDataGateway gameDataGateway, IPublishMessageAdaptor publishMessageAdaptor)
        {
            _gameDataGateway = gameDataGateway;
            _publishMessageAdaptor = publishMessageAdaptor;
        }
        public void Execute(string sessionID, string messageID, IGameData gameData)
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
            _gameDataGateway.Save(sessionID,gameData);
            _publishMessageAdaptor.Publish(new GameResponse
            {
                SessionID = sessionID,
                MessageID = messageID,
                CardsToAdd = gameData.CardsToAdd,
                CurrentCardID = gameData.CurrentCardID,
                Seed = gameData.Seed,
                PackVersion = gameData.PackVersion,
                CurrentStats = gameData.CurrentStats
            });

        }
        
        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}