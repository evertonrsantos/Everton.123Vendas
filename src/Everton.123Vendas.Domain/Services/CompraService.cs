using Everton._123Vendas.Domain.Entities;
using Everton._123Vendas.Domain.Entities.EventMessage;
using Everton._123Vendas.Domain.Interfaces.Events;
using Everton._123Vendas.Domain.Interfaces.Repositories;
using Everton._123Vendas.Domain.Interfaces.Services;

namespace Everton._123Vendas.Domain.Services
{
    public class CompraService : ServiceBase<Compra>, ICompraService
    {
        private readonly ICompraCriadaPublisher _compraCriadaPublisher;
        private readonly ICompraRepository _repository;

        public CompraService(ICompraRepository repository, ICompraCriadaPublisher compraCriadaPublisher) : base(repository)
        {
            _repository = repository;
            _compraCriadaPublisher = compraCriadaPublisher;
        }

        public override async Task<Guid> CreateAsync(Compra entity)
        {
            if (!entity.Validar()) return Guid.Empty;

            entity.AplicarDesconto();
            var id = await base.CreateAsync(entity);
            
            _ = _compraCriadaPublisher.PublishAsync(new CompraCriadaMessage(entity));

            return id;
        }
    }
}
