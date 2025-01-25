using BankAccountTransactions.Domain;
using BankAccountTransactions.Domain.Interface;
using BankAccountTransactionsDomain_.Enum;

namespace BankAccountTransactionsDomain.Entity
{
    public class User : IEntity<Guid>
    {
        public User(Guid id, string fullName, string email, string password, string document, Account account, UserType userType) : base(id)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Document = document;
            Account = account;
            UserType = userType;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Document { get; set; }
        public Account Account { get; set; }
        public UserType UserType{ get; set; }

    }
}
