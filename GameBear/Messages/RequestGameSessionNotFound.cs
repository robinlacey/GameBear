using Messages;

namespace GameBear.Messages
{
    public class RequestGameSessionNotFound:IRequestGameSessionNotFound
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }
    }
}