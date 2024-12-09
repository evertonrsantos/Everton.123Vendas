using Everton._123Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace Everton._123Vendas.Infrastructure.Data.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ItemCompra> ItensCompra { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(256);

            configurationBuilder.Properties<decimal>()
                .HavePrecision(18, 2);
            
            configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp");

            configurationBuilder.Conventions.Remove(typeof(ManyToManyCascadeDeleteConvention));
            configurationBuilder.Conventions.Remove(typeof(OneToManyCascadeDeleteConvention));
            configurationBuilder.Conventions.Remove(typeof(PluralizingTableNameConvention));

            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
