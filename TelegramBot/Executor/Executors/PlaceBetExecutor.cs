using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class PlaceBetExecutor : IExecutor<PlaceBetCommandType>
    {
        private readonly IUserCommandStateService stateService;
        private readonly ITelegramBotClient client;

        public PlaceBetExecutor(ITelegramBotClient botClient, IUserCommandStateService stateService)
        {
            this.client = botClient;
            this.stateService = stateService;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            var state = stateService.GetCurrentState(message.User);

            if (state is PlaceBetCommandState betState)
            {
                switch (betState.State)
                {
                    case PlaceBetState.EventNumber:
                        if (true) //если введен существующий номер
                        {
                            //вывод информации о выбранном событии
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "информация о выбранном событии \n Введите номер линии, на которую вы хотите сделать ставку");
                            stateService.SaveState(message.User, new PlaceBetCommandState(PlaceBetState.LineNumber));
                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "События с таким номером нет, пожалуйста, уточните номер и введите еще раз");
                        }

                        break;
                    case PlaceBetState.LineNumber:
                        if (true) //если введен существующий номер линии
                        {
                            //вывод информации о выбранной линии
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "информация о выбранной линии \n Введите номер исхода, на который вы хотите сделать ставку");
                            stateService.SaveState(message.User, new PlaceBetCommandState(PlaceBetState.OutcomeNumber));
                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "На это событие нет линии с таким номером, пожалуйста, уточните номер и введите еще раз");
                        }
                        break;
                    case PlaceBetState.OutcomeNumber:
                        if (true) //если введен существующий номер исхода
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Введите размер ставки");
                            stateService.SaveState(message.User, new PlaceBetCommandState(PlaceBetState.BetAmount));
                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Нет исхода с таким номером, пожалуйста, уточните номер и введите еще раз");
                        }
                        break;
                    case PlaceBetState.BetAmount:
                        if (true) //если введена нормальная сумма ставки
                        {
                            //сохранить эту ставку
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "Ваша ставка сохранена");
                            stateService.DeleteState(message.User);
                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                                "На вашем счету недостаточно средств, введте новую сумму ставки");
                        }
                        break;
                }
            }
            else
            {
                //вывести список событий
                await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                    "Список событий \n Введите номер события, на которое хотите сделать ставку");
                stateService.SaveState(message.User, new PlaceBetCommandState(PlaceBetState.EventNumber));
            }


            await client.SendTextMessageAsync(message.TelegramMessage.Chat, $"Called PlaceBetExecutor with message {message.TelegramMessage.Text} " +
                                                                          $"and state {stateService.GetCurrentState(message.User)}");
        }

    }
}
