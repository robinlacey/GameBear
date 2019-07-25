using System;

namespace DealerBear.Messages
{
    public interface IGameResponse
    {
        string SessionID { get; set; }
        string MessageID { get; set; }
        string CurrentCardID { get; set; }
        int Seed { get; set; }
        int PackVersion { get; set; }
        Tuple<string, float>[] CardsToAdd { get; set; }
    }
}