using Everton._123Vendas.Domain.Entities;
using Everton._123Vendas.Domain.Interfaces.Repositories;

namespace Everton._123Vendas.Infrastructure.Data.Repository
{
    public class ItemCompraRepository : RepositoryBase<ItemCompra>, IItemCompraRepository
    {
        public ItemCompraRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(IEnumerable<ItemCompra> items)
        {
            _dbSet.AddRange(items);
            _context.SaveChanges();
        }

        public async Task RemoveRangeAsync(IEnumerable<ItemCompra> items)
        {
            _dbSet.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}
