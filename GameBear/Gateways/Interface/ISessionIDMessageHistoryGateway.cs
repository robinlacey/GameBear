namespace GameBear.Gateways.Interface
{
    public interface ISessionIDMessageHistoryGateway
    {
        string[] GetMessageIDHistory(string sessionID);
        void AddMessageIDToHistory(string messageID);
    }
}