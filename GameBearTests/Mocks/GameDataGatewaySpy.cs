using GameBear.Data;
using GameBear.Gateways.Interface;

namespace GameBearTests.Mocks
{
    public class GameDataGatewaySpy : IGameDataGateway
    {
        public string GetGameDataSessionID { get; private set; }

        public IGameData Get(string sessionID)
        {
            GetGameDataSessionID = sessionID;
            return null;
        }

        public string IsExistingSessionSessionID { get; private set; }

        public bool IsExistingSession(string sessionID)
        {
            IsExistingSessionSessionID = sessionID;
            return false;
        }
        public string SaveSessionID { get; private set; }
        public IGameData SaveGameData { get; private set; }
        public void Save(string sessionID, IGameData data)
        {
            SaveSessionID = sessionID;
            SaveGameData = data;
        }
    }
}