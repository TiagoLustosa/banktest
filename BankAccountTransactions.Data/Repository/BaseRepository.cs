using BankAccountTransactions.Data.Context;
using BankAccountTransactions.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankAccountTransactions.Domain.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly BankAccountTransactionsContext _context;

        protected RepositoryBase(BankAccountTransactionsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T?> GetByDocument(string userDocument)
        {
            return await _context.Set<T>().FindAsync(userDocument);
        }
        public async Task Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
