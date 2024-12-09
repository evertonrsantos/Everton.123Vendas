using Everton._123Vendas.Domain.Entities;

namespace Everton._123Vendas.Domain.Interfaces.Repositories
{
    public interface IItemCompraRepository : IRepositoryBase<ItemCompra>
    {
        Task RemoveRangeAsync(IEnumerable<ItemCompra> items);
        Task AddRangeAsync(IEnumerable<ItemCompra> items);
    }
}
