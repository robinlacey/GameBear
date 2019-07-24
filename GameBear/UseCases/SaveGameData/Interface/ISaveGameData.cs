using GameBear.Data;
using GameBear.Gateways.Interface;

namespace GameBear.UseCases.SaveGameData.Interface
{
    public interface ISaveGameData
    {
        void Execute(string sessionID, string messageID, ISessionIDMessageHistoryGateway messageHistoryGateway, IGameData gameData);
    }
}