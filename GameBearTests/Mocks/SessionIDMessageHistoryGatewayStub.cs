using GameBear.Gateways.Interface;

namespace GameBearTests.Mocks
{
    public class SessionIDMessageHistoryGatewayStub:ISessionIDMessageHistoryGateway
    {
        public string[] MessageIDHsitory { get; }

        public SessionIDMessageHistoryGatewayStub(string[] messageIDHsitory)
        {
            MessageIDHsitory = messageIDHsitory;
        }

        public string[] GetMessageIDHistory(string sessionID) => MessageIDHsitory;
    

        public void AddMessageIDToHistory(string messageID)
        {
           
        }
    }
}