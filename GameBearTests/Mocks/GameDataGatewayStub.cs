using GameBear.Data;
using GameBear.Gateways.Interface;

namespace GameBearTests.Mocks
{
    public class GameDataGatewayStub : IGameDataGateway
    {
        private readonly IGameData _getGameDataReturn;
        private readonly bool _isExistingSessionReturn;

        public GameDataGatewayStub(IGameData getGameDataReturn, bool isExistingSessionReturn)
        {
            _getGameDataReturn = getGameDataReturn;
            _isExistingSessionReturn = isExistingSessionReturn;
        }

        public IGameData Get(string sessionID) => _getGameDataReturn;

        public bool IsExistingSession(string sessionID) => _isExistingSessionReturn;
        public void Save(string sessionID, IGameData data) { }
    }
}