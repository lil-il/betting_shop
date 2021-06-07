using System;
using System.Collections.Concurrent;
using System.Linq;
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
            var userClient = new UserClient("http://localhost:27254");
            if (state is PlaceBetCommandState betState)
            {
                switch (betState.State)
                {
                    case PlaceBetState.EventNumber:
                        var allEvents = await eventClient.GetAllAsync();
                        int chosenEventNumber;
                        if (!Int32.TryParse(message.Tail, out chosenEventNumber))
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "вводи цифры долбаеб");
                            break;
                        }
                        if (chosenEventNumber < 1 || chosenEventNumber > allEvents.Length)
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Введите, пожалуйста, существующий номер ставки");
                            break;
                        }
                        var chosenEvent = await eventClient.GetAsync((idDictionary[chosenEventNumber]));
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            $"Событие {chosenEventNumber}  - {chosenEvent.Name}\n" +
                            $"Исходы: {chosenEvent.Outcomes}\n" +
                            $"Дедлайн: {chosenEvent.BetDeadline}\n" +
                            $"Описание: {chosenEvent.Description}\n" +
                            $"----------------\n" +
                            $" Введите исход, на которую вы хотите сделать ставку");
                        stateService.SaveState(message.User, new PlaceBetCommandState() { State = PlaceBetState.Outcome, EventId = chosenEvent.Id });
                        break;
                    case PlaceBetState.Outcome:
                        var chosenOutcome = message.Tail;
                        var myEvent = await eventClient.GetAsync(betState.EventId);
                        var myEventOutcomes = myEvent.Outcomes.Split('\n');
                        if (!myEventOutcomes.Contains(chosenOutcome))
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Введите, пожалуйста, существующий исход");
                            break;
                        }
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Введите размер ставки");
                        stateService.SaveState(message.User, new PlaceBetCommandState() { State = PlaceBetState.BetAmount, EventId = betState.EventId, Outcome = chosenOutcome });
                        break;
                    case PlaceBetState.BetAmount:
                        int betAmount;
                        if (!Int32.TryParse(message.Tail, out betAmount))
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "вводи цифры долбаеб");
                            break;
                        }
                        var user = await userClient.GetByTelegramIdAsync(message.TelegramMessage.From.Id);
                        var balance = user.Balance;
                        if (betAmount < 1 || balance < betAmount)
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Вы ввели некорректный размер ставки, размер ставки не должен превышать ваш баланс");
                            break;
                        }
                        var bet = await betClient.CreateAsync(new BetMeta { BetSize = betAmount, EventId = betState.EventId, UserId = user.Id, Outcome = betState.Outcome });//поправить юзер айди
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