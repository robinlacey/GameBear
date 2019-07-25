using System;
using GameBear.Data;

namespace GameBearTests.Mocks
{
    public class GameDataDummy:IGameData
    {
        public string SessionID { get; set; }
        public string CurrentCardID { get; set; }
        public int Seed { get; }
        public int PackVersion { get; }
        public Tuple<string, float>[] CardsToAdd { get; set; }
    }
}