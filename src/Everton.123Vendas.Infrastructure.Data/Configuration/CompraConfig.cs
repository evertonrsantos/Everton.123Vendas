using Everton._123Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Everton._123Vendas.Infrastructure.Data.Configuration
{
    public class CompraConfig : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("Compra");
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.NumeroVenda)
                .IsRequired();

            builder
                .Property(x => x.DataVenda)
                .IsRequired();

            builder
                .Property(x => x.ValorTotal)
                .IsRequired();

            builder
                .Property(x => x.CodigoLoja)
                .IsRequired();

            builder
                .Property(x => x.Cancelada)
                .IsRequired();

            builder
                .Property(x => x.ClienteId)
                .IsRequired();

            builder
                .HasMany(x => x.Itens)
                .WithOne(x => x.Compra);
        }
    }
}
