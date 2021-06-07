using System;
using System.Collections.Concurrent;

namespace BettingShop.TelegramBot.Command.Commands
{
    public enum CloseEventState
    {
        EventChoosing = 0,
        WinningOutcome = 1,
    }

    public class CloseEventCommandState : ICommandState<CloseEventCommandType>
    {
        public CloseEventState State { get; set; }
        public Guid ChosenEventId { get; set; }
        public ConcurrentDictionary<int, Guid> IdDictionary { get; set; }
    }
}