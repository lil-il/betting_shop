
using System.Linq;
using System.Reflection;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.Executor;
using BettingShop.TelegramBot.Executor.Executors;
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
            return Assembly.GetCallingAssembly().GetTypes().Where(T =>
                    T.BaseType.IsAssignableFrom(typeof(IExecutor)) &&
                    T.BaseType.GenericTypeArguments.First().Equals(commandType.GetType()))
                .Select(T => container.GetInstance(T)).Cast<IExecutor>().First();
        }
    }
}
