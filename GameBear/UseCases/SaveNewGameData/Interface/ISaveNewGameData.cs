using DealerBear.Messages;
using GameBear.Gateways.Interface;

namespace GameBear.UseCases.SaveNewGameData.Interface
{
    public interface ISaveNewGameData
    {
        void Execute(ICreateNewGameData newGameData, IGameDataGateway gateway);
    }
}