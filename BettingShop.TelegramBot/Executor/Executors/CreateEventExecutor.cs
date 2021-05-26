using System;
using System.Threading.Tasks;
using BetEvent.Api.Client;
using BetEvent.Api.Client.Models;
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
                            $"Введите возможные исходы вашего события, имя:{createState.Forming.Name}");
                        stateService.SaveState(message.User, new CreateEventCommandState() { State = CreateEventState.Outcomes });

                        break;
                    case CreateEventState.Outcomes:
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat, 
                            $"Введите дату и время окончания события,  имя:{createState.Forming.Name}");
                        stateService.SaveState(message.User, new CreateEventCommandState() { State = CreateEventState.Deadline });
                        break;
                    case CreateEventState.Deadline:
                        var date = DateTime.Parse(message.Tail);
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            $"Напишите описание своего события, если не хотите, поставьте \"-\",  имя:{createState.Forming.Name}, дедлайн: {createState.Forming.BetDeadline}");
                        stateService.SaveState(message.User, new CreateEventCommandState() { State = CreateEventState.Description });
                        break;
                    case CreateEventState.Description:
                        //сгенерить событие
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            $"Ваше событие сохранено,  имя:{createState.Forming.Name}, дедлайн: {createState.Forming.BetDeadline}, описание:{createState.Forming.Description}");
                        stateService.DeleteState(message.User);
                        break;
                }
            }
            else
            {
                await client.SendTextMessageAsync(message.TelegramMessage.Chat, "Введите название события");
                stateService.SaveState(message.User, new CreateEventCommandState() { State = CreateEventState.Name });
            }

            await client.SendTextMessageAsync(message.TelegramMessage.Chat, $"Called CreateEventExecutor with message {message.TelegramMessage.Text} " +
                                                                           $"and state {stateService.GetCurrentState(message.User)}");
        }

    }
}
