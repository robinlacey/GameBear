using Messages;

namespace GameBearTests.Mocks
{
    public class RequestGameIsSessionIDInUseStub : IRequestGameIsSessionIDInUse
    {
        public RequestGameIsSessionIDInUseStub(string sessionID, string messageId)
        {
            SessionID = sessionID;
            MessageID = messageId;
        }

        public string SessionID { get; set; }
        public string MessageID { get; set; }
    }
}