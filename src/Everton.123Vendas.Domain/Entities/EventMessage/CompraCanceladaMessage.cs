namespace Everton._123Vendas.Domain.Entities.EventMessage
{
    public class CompraCanceladaMessage : EventMessageBase
    {
        public CompraCanceladaMessage(Compra compra)
        {
            CompraId = compra.Id;
            ClienteId = compra.ClienteId;
            ValorTotal = compra.ValorTotal;
        }

        public Guid CompraId { get; set; }
        public string ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        //Adicionar todos os outros campos necessários para o envio da mensagem/evento.
    }
}
