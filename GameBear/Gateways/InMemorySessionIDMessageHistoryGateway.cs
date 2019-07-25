using GameBear.Gateways.Interface;

namespace GameBear.Gateways
{
    public class InMemorySessionIDMessageHistoryGateway:ISessionIDMessageHistoryGateway
    {
        public string[] GetMessageIDHistory(string sessionID)
        {
            throw new System.NotImplementedException();
        }

        public void AddMessageIDToHistory(string messageID)
        {
            throw new System.NotImplementedException();
        }
    }
}