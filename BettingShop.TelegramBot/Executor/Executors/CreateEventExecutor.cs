using System;
using System.Threading.Tasks;
using BettingShop.Api.Client;
using BettingShop.Api.Client.Models;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;


namespace BettingShop.TelegramBot.Executor.Executors
{
    public class CreateEventExecutor : IExecutor<CreateEventCommandType>
    {
        private readonly IUserCommandStateService stateService;
        private readonly ITelegramBotClient client;

        public CreateEventExecutor(ITelegramBotClient botClient, IUserCommandStateService stateService)
        {
            this.client = botClient;
            this.stateService = stateService;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            var state = stateService.GetCurrentState(message.User);
            var eventClient = new BetEventClient("http://localhost:27254");
            if (state is CreateEventCommandState createState)
            {
                switch (createState.State)
                {
                    case CreateEventState.Name:
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            $"Введите возможные исходы вашего события");
                        stateService.SaveState(message.User, new CreateEventCommandState() { State = CreateEventState.Outcomes, Name = message.Tail });

                        break;
                    case CreateEventState.Outcomes:
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            $"Введите дату и время окончания события");
                        stateService.SaveState(message.User, new CreateEventCommandState() { State = CreateEventState.Deadline, Name = createState.Name, Outcomes = message.Tail });
                        break;
                    case CreateEventState.Deadline:
                        var date = DateTime.Parse(message.Tail);
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            $"Напишите описание своего события, если не хотите, поставьте \"-\"");
                        stateService.SaveState(message.User, new CreateEventCommandState() { State = CreateEventState.Description, Name = createState.Name, Outcomes = createState.Outcomes, Deadline = date });
                        break;
                    case CreateEventState.Description:
                        await eventClient.CreateAsync(new BetEventMeta { Name = createState.Name, BetDeadline = createState.Deadline, Outcomes = createState.Outcomes, Description = message.Tail });
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            $"Ваше событие сохранено");
                        stateService.DeleteState(message.User);
                        break;
                }
            }
            else
            {
                await client.SendTextMessageAsync(message.TelegramMessage.Chat, "Введите название события");
                stateService.SaveState(message.User, new CreateEventCommandState() { State = CreateEventState.Name });
            }
        }

    }
}
