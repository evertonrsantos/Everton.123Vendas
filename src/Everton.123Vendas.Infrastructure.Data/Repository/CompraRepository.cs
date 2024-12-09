using Everton._123Vendas.Domain.Entities;
using Everton._123Vendas.Domain.Interfaces.Repositories;

namespace Everton._123Vendas.Infrastructure.Data.Repository
{
    public class CompraRepository : RepositoryBase<Compra>, ICompraRepository
    {
        public CompraRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
