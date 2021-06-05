using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using BettingShop.Api.Client;
using BettingShop.Api.Client.Models;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class PlaceBetExecutor : IExecutor<PlaceBetCommandType>
    {
        private readonly IUserCommandStateService stateService;
        private readonly ITelegramBotClient client;
        private ConcurrentDictionary<int, Guid> idDictionary = new ConcurrentDictionary<int, Guid>();

        public PlaceBetExecutor(ITelegramBotClient botClient, IUserCommandStateService stateService)
        {
            this.client = botClient;
            this.stateService = stateService;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            var state = stateService.GetCurrentState(message.User);
            var eventClient = new BetEventClient("http://localhost:27254");
            var betClient = new BetClient("http://localhost:27254");
            if (state is PlaceBetCommandState betState)
            {
                switch (betState.State)// нужно проверять на корректность ввода
                {
                    case PlaceBetState.EventNumber:
                        var chosenEvent = await eventClient.GetAsync((idDictionary[int.Parse(message.Tail)]));
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat, 
                            $"Событие {int.Parse(message.Tail)}  - {chosenEvent.Name}\n" +
                            $"Исходы: {chosenEvent.Outcomes}\n" +
                            $"Дедлайн: {chosenEvent.BetDeadline}\n" +
                            $"Описание: {chosenEvent.Description}\n" +
                            $"----------------\n" +
                            $" Введите исход, на которую вы хотите сделать ставку");
                        stateService.SaveState(message.User, new PlaceBetCommandState() { State = PlaceBetState.Outcome, EventId = chosenEvent.Id});
                        break;
                    case PlaceBetState.Outcome:
                        var chosenOutcome = message.Tail;
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Введите размер ставки");
                        stateService.SaveState(message.User, new PlaceBetCommandState(){ State = PlaceBetState.BetAmount, EventId = betState.EventId, Outcome = chosenOutcome});
                        break;
                    case PlaceBetState.BetAmount:
                        var betAmount = Int16.Parse(message.Tail);
                        await betClient.CreateAsync(new BetMeta { BetSize = betAmount, EventId = betState.EventId, UserId = Guid.NewGuid(), Outcome = betState.Outcome });//поправить юзер айди
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            "Ваша ставка сохранена");
                        stateService.DeleteState(message.User);
                        break;
                }
            }
            else
            {
                var allEvents = await eventClient.GetAllAsync();
                var allEventsString = new StringBuilder();
                var i = 1;
                foreach (var oneEvent in allEvents)
                {
                    idDictionary[i] = oneEvent.Id;
                    allEventsString.Append($"{i} - {oneEvent.Name}\n" +
                                           $"Исходы: {oneEvent.Outcomes}\n" +
                                           $"Дедлайн: {oneEvent.BetDeadline}\n" +
                                           $"Описание: {oneEvent.Description}\n" +
                                           $"----------------\n");
                    i++;
                }
                await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                    $"{allEventsString.ToString()} \n Введите номер события, на которое хотите сделать ставку");
                stateService.SaveState(message.User, new PlaceBetCommandState() { State = PlaceBetState.EventNumber });
            }
        }

    }
}
