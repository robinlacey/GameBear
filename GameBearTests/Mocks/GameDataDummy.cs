using System;
using GameBear.Data;

namespace GameBearTests.Mocks
{
    public class GameDataDummy:IGameData
    {
        public string CurrentCardID { get; set; }
        public int Seed { get; set; }
        public int PackVersion { get;set; }
        public Tuple<string, float>[] CardsToAdd { get; set; }
    }
}