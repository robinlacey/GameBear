using DealerBear.Adaptor.Interface;
using DealerBear.Messages;
using GameBear.Gateways.Interface;

namespace GameBear.UseCases.FetchInProgressGameData.Interface
{
    public interface IFetchInProgressGameData
    {
        IGameResponse Execute(string sessionID, IGameDataGateway gateway,IPublishMessageAdaptor publishMessageAdaptor);
    }
}