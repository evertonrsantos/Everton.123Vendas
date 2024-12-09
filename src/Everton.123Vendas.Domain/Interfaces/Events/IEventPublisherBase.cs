using Everton._123Vendas.Domain.Entities.EventMessage;

namespace Everton._123Vendas.Domain.Interfaces.Events
{
    public interface IEventPublisherBase<T> where T : EventMessageBase
    {
        Task PublishAsync(T message);
    }
}
