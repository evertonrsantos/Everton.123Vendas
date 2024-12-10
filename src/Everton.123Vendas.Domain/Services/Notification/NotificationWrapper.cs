using Everton._123Vendas.Domain.Interfaces.Notification;

namespace Everton._123Vendas.Domain.Services.Notification
{
    public static class NotificationWrapper
    {
        public static bool IsValid => !GetService().HasNotification;

        public static void Add(string context, string message)
        {
            GetService().AddError(context, message);
        }

        private static INotification GetService()
        {
            return (INotification)ServiceLocator.Provider.GetService(typeof(INotification));
        }
    }
}
