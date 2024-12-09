using Everton._123Vendas.Domain.Interfaces.Notification;

namespace Everton._123Vendas.Domain.Services.Notification
{
    public static class NotificationWrapper
    {
        public static bool IsValid => !GetContainer().HasNotification;

        public static void Add(string context, string message)
        {
            GetContainer().AddError(context, message);
        }

        private static INotification GetContainer()
        {
            return ServiceLocator.Container.GetService<INotification>(typeof(INotification));
        }
    }
}
