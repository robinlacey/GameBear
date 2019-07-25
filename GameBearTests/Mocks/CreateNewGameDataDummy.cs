using DealerBear.Messages;

namespace GameBearTests.Mocks
{
    public class CreateNewGameDataDummy:ICreateNewGameData
    {
        public string SessionID { get; }
        public string MessageID { get; }
        public int Seed { get; }
        public int PackVersionNumber { get; }
        public string CurrentCard { get; set; }
    }
}