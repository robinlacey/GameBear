using DealerBear.Messages;
using GameBear.Gateways.Interface;
using GameBear.UseCases.SaveGameData.Interface;
using GameBear.UseCases.SaveNewGameData.Interface;

namespace GameBear.UseCases.SaveNewGameData
{
    public class SaveNewGameData:ISaveNewGameData
    {
        private readonly ISaveGameData _saveGameData;

        public SaveNewGameData(ISaveGameData saveGameData)
        {
            _saveGameData = saveGameData;
        }
        public void Execute(ICreateNewGameData newGameData, IGameDataGateway gateway)
        {
            throw new System.NotImplementedException();
        }
    }
}