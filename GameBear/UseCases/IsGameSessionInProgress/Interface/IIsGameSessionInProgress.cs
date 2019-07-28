namespace GameBear.UseCases.RequestGameCheckExistingSession.Interface
{
    public interface IIsGameSessionInProgress
    {
        void Execute(string sessionID, string messageID);
    }
}