using Everton._123Vendas.Domain.Interfaces.Notification;

namespace Everton._123Vendas.Domain.Services.Notification
{
    public static class ServiceLocator
    {
        public static IServiceProvider Provider { get; private set; }

        public static void Initialize(IServiceProvider provider)
        {
            Provider = provider;
        }
    }
}
