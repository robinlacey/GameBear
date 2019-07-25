using System.Collections.Generic;
using System.Linq;
using DealerBear.Adaptor.Interface;
using GameBear.Data;
using GameBear.Exceptions;
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
            Dictionary<string,int> startingStats,
            ISaveNewGameData saveNewGameDataUseCase, 
            ISessionIDMessageHistoryGateway messageHistoryGateway,
            IGameDataGateway gameDataGateway, 
            IPublishMessageAdaptor publishMessageAdaptor)
        {
            
            // Check session ID and Message ID
            if (InvalidIDString(sessionID))
            {
                throw new InvalidSessionIDException();
            }
            if (InvalidIDString(messageID))
            {
                throw new InvalidMessageIDException();
            }

            if (startingStats == null || startingStats.Keys.Count == 0 || StartingStatsHasInvalidKeys(startingStats))
            {
                throw new InvalidStartingStatsException();
            }
            if (!gameDataGateway.IsExistingSession(sessionID) || MessageIDIsInHistory(sessionID, messageID, messageHistoryGateway))
            {
                return;
            }
            messageHistoryGateway.AddMessageIDToHistory(sessionID);
            saveNewGameDataUseCase.Execute(
                sessionID,
                messageID,
                new GameData
            {
                CurrentCardID = currentCard,
                Seed = seed,
                PackVersion = packVersionNumber,
                CurrentStats = startingStats
                
            },
                gameDataGateway,publishMessageAdaptor);
        }

        bool StartingStatsHasInvalidKeys(Dictionary<string, int> startingStats)
        {
            foreach (string key in startingStats.Keys)
            {
                if (InvalidIDString(key))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool MessageIDIsInHistory(string sessionID, string messageID, ISessionIDMessageHistoryGateway messageHistoryGateway)
        {
            return messageHistoryGateway.GetMessageIDHistory(sessionID).Contains(messageID);
        }
        
        private static bool InvalidIDString(string id) => id == null || string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
    }
}