using System;
using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
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
            client = botClient;
            this.stateService = stateService;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            var state = stateService.GetCurrentState(message.User);
            if (state is CreateEventCommandState createState)
            {
                switch (createState.State)
                {
                    case CreateEventState.Name:
                        //сохранить название события
                        await client.SendTextMessageAsync(message.telegramMessage.Chat,
                            "Введите возможные исходы события");
                        stateService.SaveState(message.User, new CreateEventCommandState(CreateEventState.Lines));
                        break;
                    case CreateEventState.Lines:
                        //сохранить исходы
                        await client.SendTextMessageAsync(message.telegramMessage.Chat,
                            "Введите дату и время окончания события");
                        stateService.SaveState(message.User, new CreateEventCommandState(CreateEventState.Deadline));
                        break;
                    case CreateEventState.Deadline:
                        //сохранить дедлайн
                        await client.SendTextMessageAsync(message.telegramMessage.Chat,
                            "Напишите описание своего события, если не хотите, поставьте \"-\" ");
                        stateService.SaveState(message.User, new CreateEventCommandState(CreateEventState.Description));
                        stateService.DeleteState(message.User);
                        break;
                    case CreateEventState.Description:
                        //сохранить описание
                        //сгенерить событие
                        stateService.DeleteState(message.User);
                        break;
                }
            }
            else
            {
                await client.SendTextMessageAsync(message.telegramMessage.Chat, "Введите название события");
                stateService.SaveState(message.User, new CreateEventCommandState(CreateEventState.Name));
            }

            //await client.SendTextMessageAsync(message.telegramMessage.Chat, $"Called CreateEventExecutor with message {message.telegramMessage.Text} " +
              //                                                               $"and state {stateService.GetCurrentState(message.User)}");
        }

    }
}
