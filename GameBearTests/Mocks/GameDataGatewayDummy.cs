using GameBear.Data;
using GameBear.Gateways.Interface;

namespace GameBearTests.Mocks
{
    public class GameDataGatewayDummy : IGameDataGateway
    {
        public IGameData Get(string sessionID) => null;

        public bool IsExistingSession(string sessionID) => false;

        public void Save(string sessionID, IGameData data)
        {
            
        }
    }
}