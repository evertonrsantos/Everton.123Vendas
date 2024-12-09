using Everton._123Vendas.Domain.Services.Notification;

namespace Everton._123Vendas.Domain.Entities
{
    public class ItemCompra : EntityBase
    {
        public ItemCompra(string codigoProduto, int quantidade, decimal valorUnitario)
        {
            Id = Guid.NewGuid();
            CodigoProduto = codigoProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public string CodigoProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public decimal ValorTotal { get; private set; }

        public virtual Compra Compra { get; protected set; }

        public bool Validar()
        {
            if (Quantidade > 20)
                NotificationWrapper.Add($"item.{CodigoProduto}", "Não é possível vender acima de 20 itens iguais");

            if (Quantidade < 4 && Desconto > 0)
                NotificationWrapper.Add($"item.{CodigoProduto}", "Compras abaixo de 4 itens não podem ter desconto");

            return NotificationWrapper.IsValid;
        }

        public void Atualizar(int quantidade, decimal valorUnitario)
        {
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            AplicarDesconto();
        }

        public void AplicarDesconto()
        {
            if (Quantidade >= 10 && Quantidade <= 20)
                Desconto = 0.2m;
            else if (Quantidade >= 4)
                Desconto = 0.1m;
            else
                Desconto = 0m;

            ValorDesconto = ValorUnitario * Quantidade * Desconto;
            CalcularValorTotal();
        }

        private void CalcularValorTotal()
        {
            ValorTotal = (Quantidade * ValorUnitario) - ValorDesconto;
        }
    }
}
