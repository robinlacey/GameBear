using GameBear.Data;

namespace GameBear.UseCases.SaveGameData.Interface
{
    public interface ISaveNewGameData
    {
        void Execute(string sessionID, string messageID, IGameData gameData);
    }
}