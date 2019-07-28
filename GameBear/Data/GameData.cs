using System;
using System.Collections.Generic;

namespace GameBear.Data
{
    public class GameData:IGameData
    {
        public string CurrentCardID { get; set; }
        public int Seed { get;  set; }
        public int PackVersion { get; set;  }
        public Tuple<string, float>[] CardsToAdd { get; set; }
        public Dictionary<string, int> CurrentStats { get; set; }
    }
}