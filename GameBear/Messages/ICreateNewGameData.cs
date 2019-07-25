namespace DealerBear.Messages
{
    public interface ICreateNewGameData
    {
        string SessionID { get; }
        string MessageID { get; }
        int Seed { get; }
        int PackVersionNumber { get; }
        string CurrentCard { get; set; }
    }
}