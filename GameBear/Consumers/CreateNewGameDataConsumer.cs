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

        public CreateNewGameDataConsumer(ICheckMessageHistory checkMessageHistoryUseCase)
        {
            _checkMessageHistoryUseCase = checkMessageHistoryUseCase;
        }
        public async Task Consume(ConsumeContext<ICreateNewGameData> context)
        {
            _checkMessageHistoryUseCase.Execute(
                context.Message.SessionID,
                context.Message.MessageID,
                context.Message.Seed, 
                context.Message.PackVersionNumber,
                context.Message.CurrentCard,
                context.Message.StartingStats);
        }
    }
}