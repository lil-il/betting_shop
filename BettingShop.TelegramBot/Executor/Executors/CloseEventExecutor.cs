using System;
using System.Collections.Concurrent;
using System.Linq;
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
                        var allEvents = await eventClient.GetAllEventsFromCreatorAsync(message.TelegramMessage.From.Id);
                        int chosenEventNumber;
                        if (!Int32.TryParse(message.Tail, out chosenEventNumber))
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "введите, пожалуйста, номер цифрами");
                            break;
                        }
                        if (chosenEventNumber < 1 || chosenEventNumber > allEvents.Length)
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Введите, пожалуйста, существующий номер ставки");
                            break;
                        }
                        var chosenEvent = await eventClient.GetAsync((closeState.IdDictionary[chosenEventNumber]));
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            $"Событие {chosenEventNumber}  - {chosenEvent.Name}\n" +
                            $"Исходы: {chosenEvent.Outcomes}\n" +
                            $"Дедлайн: {chosenEvent.BetDeadline}\n" +
                            $"Описание: {chosenEvent.Description}\n" +
                            $"----------------\n" +
                            $" Выберите произошедший исход");
                        stateService.SaveState(message.User, new CloseEventCommandState() { State = CloseEventState.WinningOutcome, ChosenEventId = chosenEvent.Id});
                        break;
                    case CloseEventState.WinningOutcome:
                        var chosenOutcome = message.Tail;
                        var myEvent = await eventClient.GetAsync(closeState.ChosenEventId);
                        var myEventOutcomes = myEvent.Outcomes.Split('\n');
                        if (!myEventOutcomes.Contains(chosenOutcome))
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Введите, пожалуйста, существующий исход");
                            break;
                        }

                        await eventClient.CloseEventAsync(closeState.ChosenEventId, chosenOutcome);
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            "Событие успешно закрыто");
                        break;
                }
            }
            else
            {
                var allEvents = await eventClient.GetAllEventsFromCreatorAsync(message.TelegramMessage.From.Id);
                var allEventsString = new StringBuilder();
                var i = 1;
                var dictionary = new ConcurrentDictionary<int, Guid>();
                if (allEvents.Length == 0)
                {
                    await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                        $"Событий пока нет, чтобы предложить свое событие, напиши /createevent");
                    return;
                }
                foreach (var oneEvent in allEvents)
                {
                    dictionary[i] = oneEvent.Id;
                    allEventsString.Append($"{i} - {oneEvent.Name}\n" +
                                           $"Исходы: {oneEvent.Outcomes}\n" +
                                           $"Дедлайн: {oneEvent.BetDeadline}\n" +
                                           $"Описание: {oneEvent.Description}\n" +
                                           $"----------------\n");
                    i++;
                }
                await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                    $"{allEventsString.ToString()} \n Введите номер события, которое хотите закрыть");
                stateService.SaveState(message.User, new CloseEventCommandState() { State = CloseEventState.EventChoosing, IdDictionary = dictionary });
            }
        }
    }
}
