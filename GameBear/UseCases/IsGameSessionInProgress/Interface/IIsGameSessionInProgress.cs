using DealerBear.Adaptor.Interface;
using GameBear.Gateways.Interface;
using Messages;

namespace GameBear.UseCases.RequestGameCheckExistingSession.Interface
{
    public interface IIsGameSessionInProgress
    {
        void Execute(IRequestGameIsSessionIDInUse requestGameIsSessionIDInUse, IGameDataGateway gameDataGateway,
            IPublishMessageAdaptor publishEndpoint);
    }
}