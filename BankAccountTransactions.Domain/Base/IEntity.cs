namespace BankAccountTransactions.Domain.Interface
{
    public class IEntity<T>(T id)
    {
        public T Id { get; private set; } = id;
    }
}
