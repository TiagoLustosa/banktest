using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankAccountTransactions.Domain;
using BankAccountTransactionsDomain.Entity;

namespace BankAccountTransactions.Data.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts", "bank"); 

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired();

            builder.Property(x => x.CustomerId)
                .HasColumnName("customerId")
                .HasColumnType("uuid")
                .IsRequired(); 

            builder.Property(x => x.AccountNumber)
                .HasColumnName("accountNumber") 
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(x => x.Balance)
                .HasColumnName("balance")
                .HasColumnType("numeric");
            
            builder.HasIndex(x => x.AccountNumber).IsUnique();
            
            builder.HasOne<User>() 
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}