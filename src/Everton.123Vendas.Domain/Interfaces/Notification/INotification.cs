using Everton._123Vendas.Domain.Entities.Notification;

namespace Everton._123Vendas.Domain.Interfaces.Notification
{
    public interface INotification
    {
        public IList<NotificationError> Errors { get; set; }
        public bool HasNotification { get; }
        void AddError(string context, string message);
    }
}
