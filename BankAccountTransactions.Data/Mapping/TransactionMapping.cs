using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankAccountTransactions.Domain.Entity;

namespace BankAccountTransactions.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions", "bank");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired();

            builder.Property(x => x.SenderDocument)
                .HasColumnName("senderDocument")
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(x => x.ReceiverDocument)
                .HasColumnName("receiverDocument")
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnName("amount")
                .HasColumnType("numeric");
            builder.Property(x => x.TransactionDate)
                   .HasColumnName("transactiondate")
                   .HasColumnType("date");
                
        }
    }

}

