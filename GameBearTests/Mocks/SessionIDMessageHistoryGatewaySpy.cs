using GameBear.Gateways.Interface;

namespace GameBearTests.Mocks
{
    public class SessionIDMessageHistoryGatewaySpy : ISessionIDMessageHistoryGateway
    {
        public bool GetMessageIDHistoryCalled { get; private set; }
        public string GetMessageIDHistoryCalledSessionID { get; private set; }
        
        public bool AddMessageIDToHistoryCalled { get; private set; }
        public string AddMessageIDToHistoryMessageID { get; private set; }

        public string[] GetMessageIDHistory(string sessionID)
        {
            GetMessageIDHistoryCalled = true;
            GetMessageIDHistoryCalledSessionID = sessionID;
            return new string[0];
          
        }

        public void AddMessageIDToHistory(string messageID)
        {
            AddMessageIDToHistoryCalled = true;
            AddMessageIDToHistoryMessageID = messageID;
        }
    }
}