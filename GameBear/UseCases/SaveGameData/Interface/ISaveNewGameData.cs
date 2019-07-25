using DealerBear.Adaptor.Interface;
using GameBear.Data;
using GameBear.Gateways.Interface;

namespace GameBear.UseCases.SaveGameData.Interface
{
    public interface ISaveNewGameData
    {
        void Execute(string sessionID, string messageID, IGameData gameData, IGameDataGateway gameDataGateway, IPublishMessageAdaptor publishMessageAdaptor);
    }
}