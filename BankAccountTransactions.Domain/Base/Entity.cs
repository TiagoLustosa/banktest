namespace BankAccountTransactions.Domain.Interface
{
    public abstract class Entity<T>
    {
        public T? Id { get; private set; }

        protected Entity(T? id = default)
        {
            Id = id;
        }
    }
}
