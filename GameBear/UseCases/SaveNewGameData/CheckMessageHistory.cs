using System.Linq;
using GameBear.Data;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;
using GameBear.UseCases.SaveNewGameData.Interface;

namespace GameBear.UseCases.SaveNewGameData
{
    public class CheckMessageHistory:ICheckMessageHistory
    {
        public void Execute(
            string sessionID, 
            string messageID, 
            int seed, 
            int packVersionNumber, 
            string currentCard,
            ISaveGameData saveGameDataUseCase, 
            ISessionIDMessageHistoryGateway messageHistoryGateway,
            IGameDataGateway gameDataGateway)
        {
            if (!gameDataGateway.IsExistingSession(sessionID) || MessageIDIsInHistory(sessionID, messageID, messageHistoryGateway))
            {
                return;
            }
            messageHistoryGateway.AddMessageIDToHistory(sessionID);
            saveGameDataUseCase.Execute(
                sessionID,
                new GameData
            {
                CurrentCardID = currentCard,
                Seed = seed,
                PackVersion = packVersionNumber
                
            },
                gameDataGateway);
        }

        private static bool MessageIDIsInHistory(string sessionID, string messageID, ISessionIDMessageHistoryGateway messageHistoryGateway)
        {
            return messageHistoryGateway.GetMessageIDHistory(sessionID).Contains(messageID);
        }
    }
}