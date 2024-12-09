namespace Everton._123Vendas.Domain.Entities.EventMessage
{
    public class EventMessageBase
    {
        public EventMessageBase()
        {
            EventId = Guid.NewGuid();
        }

        public Guid EventId { get; set; }
    }
}
