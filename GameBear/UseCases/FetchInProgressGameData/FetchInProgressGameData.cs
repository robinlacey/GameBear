using System;
using DealerBear.Adaptor.Interface;
using DealerBear.Messages;
using GameBear.Gateways.Interface;
using GameBear.UseCases.FetchInProgressGameData.Interface;

namespace GameBear.UseCases.FetchInProgressGameData
{
    public class FetchInProgressGameData:IFetchInProgressGameData
    {
        private readonly IGameDataGateway _gateway;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public FetchInProgressGameData(IGameDataGateway gateway, IPublishMessageAdaptor publishMessageAdaptor)
        {
            _gateway = gateway;
            _publishMessageAdaptor = publishMessageAdaptor;
        }
        public IGameResponse Execute(string sessionID )
        {
            throw new NotImplementedException();
        }
    }
}