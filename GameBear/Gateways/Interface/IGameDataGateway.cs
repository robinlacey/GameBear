using GameBear.Data;
using Messages;

namespace GameBear.Gateways.Interface
{
    public interface IGameDataGateway
    {
        IGameData Get(string sessionID);
        bool IsExistingSession(string sessionID);
        void Save(string sessionID, IGameData data);
    }
}