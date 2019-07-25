using DealerBear.Messages;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBear.UseCases.SaveNewGameData.Interface
{
    public interface ICheckMessageHistory
    {
        void Execute(
            string sessionID, 
            string messageID, 
            int seed, 
            int packVersionNumber, 
            string currentCard,
            ISaveGameData saveGameDataUseCase, 
            ISessionIDMessageHistoryGateway messageHistoryGateway, 
            IGameDataGateway gameDataGateway );
    }
}