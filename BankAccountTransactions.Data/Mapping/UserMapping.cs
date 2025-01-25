using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankAccountTransactionsDomain.Entity;

namespace BankAccountTransactions.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", "bank");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()") 
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasColumnName("name")
                .HasColumnType("varchar(70)")
                .IsRequired(); 

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Document)
                .HasColumnName("document")
                .HasColumnType("varchar(18)")
                .IsRequired();
       
            builder.HasIndex(x => x.Document)
                .IsUnique()
                .HasDatabaseName("IX_User_Document");

            builder.Property(x => x.AccountId)
                .HasColumnName("accountId")
                .HasColumnType("uuid");

            builder.Property(x => x.UserType)
                .HasColumnName("userType")
                .HasColumnType("varchar(2)")
                .IsRequired();

        }
    }
}