namespace Everton._123Vendas.API.Models.Response
{
    public class ItemCompraResponse
    {
        public Guid Id { get; set; }
        public string CodigoProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
    }
}
