using GameBear.Data;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBearTests.Mocks
{
    public class SaveGameDataDummy:ISaveGameData
    {
        public void Execute(string sessionID, IGameData gameData, IGameDataGateway gameDataGateway)
        {
            
        }
    }
}