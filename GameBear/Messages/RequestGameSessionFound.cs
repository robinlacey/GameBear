using Messages;

namespace GameBear.Messages
{
    public class RequestGameSessionFound:IRequestGameSessionFound
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }
    }
}