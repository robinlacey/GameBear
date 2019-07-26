using GameBear.Data;
using GameBear.Gateways.Interface;

namespace GameBear.Gateways
{
    public class InMemoryGameDataGateway : IGameDataGateway
    {
        public IGameData Get(string sessionID)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistingSession(string sessionID)
        {
            throw new System.NotImplementedException();
        }

        public void Save(string sessionID, IGameData data)
        {
            throw new System.NotImplementedException();
        }
    }
}