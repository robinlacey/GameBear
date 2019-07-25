using System.Threading.Tasks;
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
        private readonly ISaveGameData _saveGameData;
        private readonly ISessionIDMessageHistoryGateway _sessionIDMessageHistoryGateway;

        public CreateNewGameDataConsumer(ICheckMessageHistory checkMessageHistoryUseCase, IGameDataGateway gameDataGateway,ISaveGameData saveGameData, ISessionIDMessageHistoryGateway sessionIDMessageHistoryGateway)
        {
            _saveGameData = saveGameData;
            _sessionIDMessageHistoryGateway = sessionIDMessageHistoryGateway;
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
                _saveGameData,
                _sessionIDMessageHistoryGateway,
                _gameDataGateway );
        }
    }
}