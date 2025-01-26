using BankAccountTransactions.Domain.Interface;
using BankAccountTransactions.Domain.Enum;

namespace BankAccountTransactions.Domain.Entity
{
    public class User : Entity<Guid>
    {
        public User()
        {
            
        }
        public User(Guid id, string fullName, string email, string password, string document, Guid accountId, Account account, UserType userType) : base(id)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Document = document;
            AccountId = accountId;
            Account = account;
            UserType = userType;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Document { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public UserType UserType { get; set; }

    }
}
