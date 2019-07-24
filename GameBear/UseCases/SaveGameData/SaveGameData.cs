using GameBear.Data;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBear.UseCases.SaveGameData
{
    public class SaveGameData:ISaveGameData
    {
        public void Execute(string sessionID, string messageID, ISessionIDMessageHistoryGateway messageHistoryGateway,
            IGameData gameData)
        {
            throw new System.NotImplementedException();
        }
    }
}