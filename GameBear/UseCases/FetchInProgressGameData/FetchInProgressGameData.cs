using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Messages;
using GameBear.Gateways.Interface;
using GameBear.UseCases.FetchInProgressGameData.Interface;

namespace GameBear.UseCases.FetchInProgressGameData
{
    public class FetchInProgressGameData:IFetchInProgressGameData
    {
        public IGameResponse Execute(string sessionID, IGameDataGateway gateway,IPublishMessageAdaptor publishMessageAdaptor )
        {
            throw new NotImplementedException();
        }
    }
}