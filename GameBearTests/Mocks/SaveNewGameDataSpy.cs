using DealerBear.Adaptor.Interface;
using GameBear.Data;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;

namespace GameBearTests.Mocks
{
    public class SaveNewGameDataSpy:ISaveNewGameData
    {
        public bool ExecuteCalled { get; private set; }
        public string SessionID { get; private set; }
        public string MessageID { get; private set; }
        public IGameData GameDataToSave = new GameData();


        public void Execute(string sessionID, string messageID, IGameData gameData, IGameDataGateway gameDataGateway,
            IPublishMessageAdaptor publishMessageAdaptor)
        {
            SessionID = sessionID;
            ExecuteCalled = true;
            MessageID = messageID;
            GameDataToSave = gameData;
        }
    }
}