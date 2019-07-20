using System;
using System.Threading.Tasks;
using GameBear.Gateways.Interface;
using GameBear.UseCases.RequestGameCheckExistingSession;
using GameBear.UseCases.RequestGameCheckExistingSession.Interface;
using MassTransit;
using Messages;

namespace GameBear.Consumers
{
    public class IsExistingSessionConsumer : IConsumer<IRequestGameIsSessionIDInUse>
    {
        private readonly IGameDataGateway _gameDataGateway;
        private readonly IRequestGameCheckExistingSession _requestGameCheckExistingSessionUseCase;

        public IsExistingSessionConsumer(IGameDataGateway gameDataGateway,
            IRequestGameCheckExistingSession requestGameCheckExistingSessionUseCase)
        {
            _gameDataGateway = gameDataGateway;
            _requestGameCheckExistingSessionUseCase = requestGameCheckExistingSessionUseCase;
        }

        public async Task Consume(ConsumeContext<IRequestGameIsSessionIDInUse> context)
        {
            _requestGameCheckExistingSessionUseCase.Execute(context.Message, _gameDataGateway, context);
        }
    }
}