using Everton._123Vendas.Domain.Entities.EventMessage;
using Everton._123Vendas.Domain.Interfaces.Events;
using Microsoft.Extensions.Logging;

namespace Everton._123Vendas.Infrastructure.Events
{
    public class CompraAlteradaPublisher : EventPublisherBase<CompraAlteradaMessage>, ICompraAlteradaPublisher
    {
        public CompraAlteradaPublisher(ILogger<CompraAlteradaMessage> logger) : base(logger)
        {
        }
    }
}
