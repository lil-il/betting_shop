﻿using System.Threading.Tasks;
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
            if (state is CreateEventCommandState createState)
            {
                switch (createState.State)
                {
                    case CreateEventState.Name:
                        createState.Forming.Name = message.Tail; 
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            "Введите возможные исходы вашего события");
                        stateService.SaveState(message.User, new CreateEventCommandState(CreateEventState.Outcomes));
                        break;
                    case CreateEventState.Outcomes:
                        createState.Forming.Outcomes = message.Tail;
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            "Введите дату и время окончания события");
                        stateService.SaveState(message.User, new CreateEventCommandState(CreateEventState.Deadline));
                        break;
                    case CreateEventState.Deadline:
                        createState.Forming.Deadline = message.Tail;
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            "Напишите описание своего события, если не хотите, поставьте \"-\" ");
                        stateService.SaveState(message.User, new CreateEventCommandState(CreateEventState.Description));
                        break;
                    case CreateEventState.Description:
                        createState.Forming.Description = message.Tail;
                        //сгенерить событие
                        await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                            "Ваше событие сохранено");
                        stateService.DeleteState(message.User);
                        break;
                }
            }
            else
            {
                await client.SendTextMessageAsync(message.TelegramMessage.Chat, "Введите название события");
                stateService.SaveState(message.User, new CreateEventCommandState(CreateEventState.Name));
            }

            await client.SendTextMessageAsync(message.TelegramMessage.Chat, $"Called CreateEventExecutor with message {message.TelegramMessage.Text} " +
                                                                           $"and state {stateService.GetCurrentState(message.User)}");
        }

    }
}
