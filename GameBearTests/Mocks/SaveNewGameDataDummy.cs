using DealerBear.Adaptor.Interface;
using GameBear.Data;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBearTests.Mocks
{
    public class SaveNewGameDataDummy:ISaveNewGameData
    {
        public void Execute(string sessionID, string messageID, IGameData gameData, IGameDataGateway gameDataGateway,
            IPublishMessageAdaptor publishMessageAdaptor)
        {
            
        }
    }
}