namespace BankAccountTransactions.Domain.Interface
{
    public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
    {
        Task<IEnumerable<TEntity?>> GetAll(TId id);
        Task<TEntity?> GetById(TId id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
    }
}
