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
            return Assembly.GetCallingAssembly().GetTypes().Where(T =>
                    T.GetInterfaces().Contains(typeof(IExecutor)) && T.GetInterfaces().Length == 2 &&
                    T.GetInterface("IExecutor`1").GenericTypeArguments.First().Equals(commandType.GetType()))
                .Select(T => container.GetInstance(T)).Cast<IExecutor>().First();
        }
    }
}
