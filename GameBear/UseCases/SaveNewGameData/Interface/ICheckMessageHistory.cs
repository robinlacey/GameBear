using System.Collections.Generic;
using DealerBear.Adaptor.Interface;
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
            Dictionary<string,int> startingStats);
    }
}