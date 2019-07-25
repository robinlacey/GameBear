using GameBear.Data;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBearTests.Mocks
{
    public class SaveGameDataSpy:ISaveGameData
    {
        public bool ExecuteCalled { get; private set; }
        public string SessionID { get; set; }
        public IGameData GameDataToSave = new GameData();
        public void Execute(
            string sessionID, IGameData gameData, IGameDataGateway gameDataGateway)
        {
            SessionID = sessionID;
            ExecuteCalled = true;
            GameDataToSave = gameData;

        }
    }
}