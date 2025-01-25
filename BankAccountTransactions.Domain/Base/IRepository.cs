namespace BankAccountTransactions.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task<T?> GetByDocument(string id);
        Task Insert(T entity);
        Task Update(T entity);
    }
}
