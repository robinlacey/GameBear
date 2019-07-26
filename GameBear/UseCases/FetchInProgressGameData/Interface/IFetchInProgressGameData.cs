using DealerBear.Messages;

namespace GameBear.UseCases.FetchInProgressGameData.Interface
{
    public interface IFetchInProgressGameData
    {
        IGameResponse Execute(string sessionID);
    }
}