using System;
using System.Collections.Concurrent;

namespace BettingShop.TelegramBot.Command.Commands
{
    public enum PlaceBetState
    { 
        EventNumber = 0,
        Outcome = 1,
        BetAmount = 2,
    }
    public class PlaceBetCommandState : ICommandState<PlaceBetCommandType>
    {
        public PlaceBetState State { get; set; }
        public Guid EventId { get; set; }
        public string Outcome { get; set; }
        public ConcurrentDictionary<int, Guid> IdDictionary { get; set; }
    }
}
