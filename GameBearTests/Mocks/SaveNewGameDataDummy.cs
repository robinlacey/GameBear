using GameBear.Data;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBearTests.Mocks
{
    public class SaveNewGameDataDummy:ISaveNewGameData
    {
        public void Execute(string sessionID, string messageID, IGameData gameData)
        {
            
        }
    }
}