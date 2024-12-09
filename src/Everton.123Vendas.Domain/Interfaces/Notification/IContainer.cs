namespace Everton._123Vendas.Domain.Interfaces.Notification
{
    public interface IContainer
    {
        T GetService<T>(Type type);
    }
}
