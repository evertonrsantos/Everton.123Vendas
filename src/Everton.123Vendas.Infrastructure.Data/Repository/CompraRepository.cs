using Everton._123Vendas.Domain.Entities;
using Everton._123Vendas.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Everton._123Vendas.Infrastructure.Data.Repository
{
    public class CompraRepository : RepositoryBase<Compra>, ICompraRepository
    {
        public CompraRepository(RepositoryContext context) : base(context)
        {
        }

        public override async Task<Compra> GetByIdAsync(Guid id)
        {
            return await _context.Compras.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
