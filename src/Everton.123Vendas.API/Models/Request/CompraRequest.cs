using System.ComponentModel.DataAnnotations;

namespace Everton._123Vendas.API.Models.Request
{
    public class CompraRequest
    {
        [Required, MinLength(2)]
        public string NumeroVenda { get; set; }
        [Required]
        public DateTime DataVenda { get; set; }
        [Required, MinLength(5)]
        public string ClienteId { get; set; }
        [Required]
        public string CodigoLoja { get; set; }
        [Required]
        public List<ItemCompraRequest> Itens { get; set; }
    }
}
