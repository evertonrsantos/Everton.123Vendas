using Everton._123Vendas.Domain.Entities.EventMessage;

namespace Everton._123Vendas.Domain.Interfaces.Events
{
    public interface ICompraCriadaPublisher : IEventPublisherBase<CompraCriadaMessage>
    {
    }
}
