﻿using Everton._123Vendas.Domain.Entities;

namespace Everton._123Vendas.Domain.Interfaces.Services
{
    public interface ICompraService : IServiceBase<Compra>
    {
        Task UpdateAsync(Guid id, Compra compra);
        Task<IEnumerable<Compra>> GetAllAsync();
    }
}
