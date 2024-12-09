using Everton._123Vendas.Domain.Entities.EventMessage;
using Everton._123Vendas.Domain.Interfaces.Events;
using Microsoft.Extensions.Logging;

namespace Everton._123Vendas.Infrastructure.Events
{
    public class CompraCriadaPublisher : EventPublisherBase<CompraCriadaMessage>, ICompraCriadaPublisher
    {
        public CompraCriadaPublisher(ILogger<CompraCriadaMessage> logger) : base(logger)
        {
        }
    }
}
