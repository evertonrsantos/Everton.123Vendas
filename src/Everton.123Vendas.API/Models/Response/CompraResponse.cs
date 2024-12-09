
namespace Everton._123Vendas.API.Models.Response
{
    public class CompraResponse
    {
        public Guid Id { get; set; }
        public string NumeroVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public string ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorTotalDesconto { get; set; }
        public string CodigoLoja { get; set; }
        public bool Cancelada { get; set; }
        public DateTime? CanceladaEm { get; set; }
    }
}
