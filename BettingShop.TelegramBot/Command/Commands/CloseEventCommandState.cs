using System;
using System.Collections.Concurrent;

namespace BettingShop.TelegramBot.Command.Commands
{
    public enum CloseEventState
    {
        EventChoosing = 0,
    }

    public class CloseEventCommandState : ICommandState<CloseEventCommandType>
    {
        public CloseEventState State { get; set; }
        public ConcurrentDictionary<int, Guid> IdDictionary { get; set; }
    }
}