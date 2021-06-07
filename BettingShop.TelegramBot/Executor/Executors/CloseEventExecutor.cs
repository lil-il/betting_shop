using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using BettingShop.Api.Client;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;


namespace BettingShop.TelegramBot.Executor.Executors
{
    public class CloseEventExecutor : IExecutor<CloseEventCommandType>
    {
        private readonly IUserCommandStateService stateService;
        private readonly ITelegramBotClient client;

        public CloseEventExecutor(ITelegramBotClient botClient, IUserCommandStateService stateService)
        {
            this.client = botClient;
            this.stateService = stateService;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            var state = stateService.GetCurrentState(message.User);
            var eventClient = new BetEventClient("http://localhost:27254");
            var betClient = new BetClient("http://localhost:27254");
            if (state is CloseEventCommandState closeState)
            {
                switch (closeState.State)
                {
                    case CloseEventState.EventChoosing:

                        break;
                }
            }
            else
            {
                //var allEvents = await eventClient.AllBetsForUserAsync(message.TelegramMessage.From.Id);
                //var allEventsString = new StringBuilder();
                //var i = 1;
                //var dictionary = new ConcurrentDictionary<int, Guid>();
                //foreach (var oneEvent in allEvents)
                //{
                //    dictionary[i] = oneEvent.Id;
                //    allEventsString.Append($"{i} - {oneEvent.Name}\n" +
                //                           $"Исходы: {oneEvent.Outcomes}\n" +
                //                           $"Дедлайн: {oneEvent.BetDeadline}\n" +
                //                           $"Описание: {oneEvent.Description}\n" +
                //                           $"----------------\n");
                //    i++;
                //}
                //await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                //    $"{allEventsString.ToString()} \n Введите номер события, которое хотите закрыть");
                //stateService.SaveState(message.User, new CloseEventCommandState() { State = CloseEventState.EventChoosing, IdDictionary = dictionary });
            }
        }
    }
}
