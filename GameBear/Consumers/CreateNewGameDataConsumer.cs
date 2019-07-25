using System.Threading.Tasks;
using DealerBear.Adaptor.Interface;
using DealerBear.Messages;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;
using GameBear.UseCases.SaveNewGameData.Interface;
using MassTransit;

namespace GameBear.Consumers
{
    public class CreateNewGameDataConsumer:IConsumer<ICreateNewGameData>
    {
        private readonly ICheckMessageHistory _checkMessageHistoryUseCase;
        private readonly IGameDataGateway _gameDataGateway;
        private readonly ISaveNewGameData _saveNewGameData;
        private readonly ISessionIDMessageHistoryGateway _sessionIDMessageHistoryGateway;
        private readonly IPublishMessageAdaptor _publishMessageAdaptor;

        public CreateNewGameDataConsumer(ICheckMessageHistory checkMessageHistoryUseCase, IGameDataGateway gameDataGateway,ISaveNewGameData saveNewGameData, ISessionIDMessageHistoryGateway sessionIDMessageHistoryGateway, IPublishMessageAdaptor publishMessageAdaptor)
        {
            _saveNewGameData = saveNewGameData;
            _sessionIDMessageHistoryGateway = sessionIDMessageHistoryGateway;
            _publishMessageAdaptor = publishMessageAdaptor;
            _checkMessageHistoryUseCase = checkMessageHistoryUseCase;
            _gameDataGateway = gameDataGateway;
        }
        public async Task Consume(ConsumeContext<ICreateNewGameData> context)
        {
            _checkMessageHistoryUseCase.Execute(
                context.Message.SessionID,
                context.Message.MessageID,
                context.Message.Seed, 
                context.Message.PackVersionNumber,
                context.Message.CurrentCard,
                _saveNewGameData,
                _sessionIDMessageHistoryGateway,
                _gameDataGateway,
                _publishMessageAdaptor );
        }
    }
}