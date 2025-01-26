using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankAccountTransactions.Domain.Entity;

namespace BankAccountTransactions.Data.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts", "bank");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Document)
                .HasColumnName("document")
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(x => x.AccountNumber)
                .HasColumnName("accountNumber")
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(x => x.Balance)
                .HasColumnName("balance")
                .HasColumnType("numeric");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("createdAt")
                .HasColumnType("timestamp");

            builder.HasIndex(x => x.AccountNumber).IsUnique();

        }
    }
}