using Everton._123Vendas.Domain.Interfaces.Notification;

namespace Everton._123Vendas.Domain.Services.Notification
{
    public static class ServiceLocator
    {
        public static IContainer Container { get; private set; }

        public static void Initialize(IContainer container)
        {
            Container = container;
        }
    }
}
