using Everton._123Vendas.Domain.Entities;
using Everton._123Vendas.Domain.Entities.EventMessage;
using Everton._123Vendas.Domain.Interfaces.Events;
using Everton._123Vendas.Domain.Interfaces.Repositories;
using Everton._123Vendas.Domain.Interfaces.Services;
using Everton._123Vendas.Domain.Services.Notification;

namespace Everton._123Vendas.Domain.Services
{
    public class CompraService : ServiceBase<Compra>, ICompraService
    {
        private readonly ICompraCriadaPublisher _compraCriadaPublisher;
        private readonly ICompraAlteradaPublisher _compraAlteradaPublisher;
        private readonly ICompraCanceladaPublisher _compraCanceladaPublisher;
        private readonly ICompraRepository _repository;
        private readonly IItemCompraRepository _itemRepository;

        public CompraService(
            ICompraRepository repository,
            IItemCompraRepository itemRepository,
            ICompraCriadaPublisher compraCriadaPublisher,
            ICompraAlteradaPublisher compraAlteradaPublisher,
            ICompraCanceladaPublisher compraCanceladaPublisher) 
            : base(repository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
            _compraCriadaPublisher = compraCriadaPublisher;
            _compraAlteradaPublisher = compraAlteradaPublisher;
            _compraCanceladaPublisher = compraCanceladaPublisher;
        }

        public override async Task<Guid> CreateAsync(Compra entity)
        {
            if (!entity.Validar()) return Guid.Empty;

            entity.AplicarDesconto();
            var id = await base.CreateAsync(entity);

            _ = _compraCriadaPublisher.PublishAsync(new CompraCriadaMessage(entity));
            return id;
        }

        public async Task UpdateAsync(Guid id, Compra compra)
        {
            if (!compra.Validar()) return;

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                NotificationWrapper.Add("compra", "Compra não encontrada");
                return;
            }

            if (compra.Itens.Count > 0)
            {
                await _itemRepository.RemoveRangeAsync(entity.Itens);
                
                entity.AlterarItens(compra.Itens);

                await _itemRepository.AddRangeAsync(entity.Itens);
                await _repository.UpdateAsync(entity);
            }
            
            _ = _compraAlteradaPublisher.PublishAsync(new CompraAlteradaMessage(entity));
        }

        public override async Task DeleteAsync(Guid id)
        {
            var compra = await _repository.GetByIdAsync(id);

            if (compra == null)
            {
                NotificationWrapper.Add("compra", "Compra não encontrada");
                return;
            }

            compra.CancelarCompra();
            await _repository.UpdateAsync(compra);

            _ = _compraCanceladaPublisher.PublishAsync(new CompraCanceladaMessage(compra));
        }

        public async Task<IEnumerable<Compra>> GetAllAsync()
        {
            return await _repository.GetAsync(x => !x.Cancelada);
        }
    }
}
