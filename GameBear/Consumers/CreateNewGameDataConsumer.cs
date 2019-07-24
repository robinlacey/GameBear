using System.Threading.Tasks;
using DealerBear.Messages;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveNewGameData.Interface;
using MassTransit;

namespace GameBear.Consumers
{
    public class CreateNewGameDataConsumer:IConsumer<ICreateNewGameData>
    {
        private readonly ISaveNewGameData _saveNewGameDataUseCase;
        private readonly IGameDataGateway _gameDataGateway;

        public CreateNewGameDataConsumer(ISaveNewGameData saveNewGameDataUseCase, IGameDataGateway gameDataGateway)
        {
            _saveNewGameDataUseCase = saveNewGameDataUseCase;
            _gameDataGateway = gameDataGateway;
        }
        public async Task Consume(ConsumeContext<ICreateNewGameData> context)
        {
            _saveNewGameDataUseCase.Execute(context.Message,_gameDataGateway );
        }
    }
}