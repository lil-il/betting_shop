using System.Linq;
using System.Reflection;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.Executor;
using LightInject;

namespace BettingShop.TelegramBot
{
    public class ExecutorsFactory
    {
        private ServiceContainer container;

        public ExecutorsFactory(ServiceContainer container)
        {
            this.container = container;
        }
        public IExecutor GetExecutor(ICommandType commandType)
        {
            return Assembly.GetCallingAssembly().GetTypes()
                .Where(T => T.IsClass)
                .Where(T => typeof(IExecutor).MakeGenericType(commandType.GetType()).IsAssignableFrom(T))
                .Select((T => container.GetInstance(T))).Cast<IExecutor>().First();
        }
    }
}
