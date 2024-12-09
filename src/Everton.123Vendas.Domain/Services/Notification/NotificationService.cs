using Everton._123Vendas.Domain.Entities.Notification;
using Everton._123Vendas.Domain.Interfaces.Notification;

namespace Everton._123Vendas.Domain.Services.Notification
{
    public class NotificationService : INotification
    {
        public NotificationService()
        {
            Errors = new List<NotificationError>();
        }

        public IList<NotificationError> Errors { get ; set; }
        public bool HasNotification { get => Errors.Any(); }

        public void AddError(string context, string message)
        {
            Errors.Add(new NotificationError { Context = context, Message = message});
        }
    }
}
