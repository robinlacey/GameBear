using System;

namespace GameBear.Data
{
    public interface IGameData
    {
        string CurrentCardID { get; set; }
        int Seed { get; }
        int PackVersion { get; }
        Tuple<string,float>[] CardsToAdd { get; set; }
    }
}