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
        private readonly ISaveNewGameData _saveNewGameDataUseCase;
        private readonly ISessionIDMessageHistoryGateway _messageHistoryGateway;
        private readonly IGameDataGateway _gameDataGateway;

        public CheckMessageHistory(ISaveNewGameData saveNewGameDataUseCase,
            ISessionIDMessageHistoryGateway messageHistoryGateway,
            IGameDataGateway gameDataGateway)
        {
            _saveNewGameDataUseCase = saveNewGameDataUseCase;
            _messageHistoryGateway = messageHistoryGateway;
            _gameDataGateway = gameDataGateway;
        }
        public void Execute(
            string sessionID, 
            string messageID, 
            int seed, 
            int packVersionNumber, 
            string currentCard,
            Dictionary<string,int> startingStats)
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
            if (!_gameDataGateway.IsExistingSession(sessionID) || MessageIDIsInHistory(sessionID, messageID, _messageHistoryGateway))
            {
                return;
            }
            _messageHistoryGateway.AddMessageIDToHistory(sessionID);
            _saveNewGameDataUseCase.Execute(
                sessionID,
                messageID,
                new GameData
            {
                CurrentCardID = currentCard,
                Seed = seed,
                PackVersion = packVersionNumber,
                CurrentStats = startingStats
                
            } );
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