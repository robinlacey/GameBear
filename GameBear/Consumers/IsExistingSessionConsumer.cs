using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;
using GameBear.Gateways.Interface;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
using MassTransit;
using Messages;

namespace GameBear.Consumers
{
    public class IsExistingSessionConsumer : IConsumer<IRequestGameIsSessionIDInUse>
    {
        private readonly IGameDataGateway _gameDataGateway;
        private readonly IIsGameSessionInProgress _isGameSessionInProgressUseCase;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public IsExistingSessionConsumer(IGameDataGateway gameDataGateway,
            IIsGameSessionInProgress isGameSessionInProgressUseCase, IPublishMessageAdaptor publishMessageAdaptor)
        {
            _gameDataGateway = gameDataGateway;
            _isGameSessionInProgressUseCase = isGameSessionInProgressUseCase;
            _publishMessageAdaptor = publishMessageAdaptor;
        }

        public async Task Consume(ConsumeContext<IRequestGameIsSessionIDInUse> context)
        {
            _isGameSessionInProgressUseCase.Execute(context.Message, _gameDataGateway, _publishMessageAdaptor);
        }
    }
}