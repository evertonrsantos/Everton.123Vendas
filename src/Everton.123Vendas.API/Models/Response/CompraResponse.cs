
namespace Everton._123Vendas.API.Models.Response
{
    public class CompraResponse
    {
        public Guid Id { get; set; }
        public string NumeroVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public string ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public string CodigoLoja { get; set; }
        public List<ItemCompraResponse> Itens { get; set; }
        public bool Cancelada { get; set; }
    }
}
