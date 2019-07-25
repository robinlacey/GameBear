using System;
using System.Collections.Generic;
using DealerBear.Messages;

namespace GameBear.Data
{
    public class GameResponse:IGameResponse
    {
        public string SessionID { get; set; }
        public string MessageID { get; set; }
        public string CurrentCardID { get; set; }
        public int Seed { get; set; }
        public int PackVersion { get; set; }
        public Dictionary<string, int> CurrentStats { get; set; }
        public Tuple<string, float>[] CardsToAdd { get; set; }
    }
}