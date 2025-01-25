using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccountTransactions.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bank");

            migrationBuilder.CreateTable(
                name: "transactions",
                schema: "bank",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    senderDocument = table.Column<string>(type: "varchar(18)", nullable: false),
                    receiverDocument = table.Column<string>(type: "varchar(18)", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "bank",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "varchar(70)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(100)", nullable: false),
                    document = table.Column<string>(type: "varchar(18)", nullable: false),
                    accountId = table.Column<Guid>(type: "uuid", nullable: false),
                    userType = table.Column<string>(type: "varchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.CheckConstraint("CK_users_userType_Enum", "\"userType\" IN ('PF', 'PJ')");
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                schema: "bank",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    customerId = table.Column<Guid>(type: "uuid", nullable: false),
                    accountNumber = table.Column<string>(type: "varchar(18)", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_accounts_users_customerId",
                        column: x => x.customerId,
                        principalSchema: "bank",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_accountNumber",
                schema: "bank",
                table: "accounts",
                column: "accountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accounts_customerId",
                schema: "bank",
                table: "accounts",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Document",
                schema: "bank",
                table: "users",
                column: "document",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts",
                schema: "bank");

            migrationBuilder.DropTable(
                name: "transactions",
                schema: "bank");

            migrationBuilder.DropTable(
                name: "users",
                schema: "bank");
        }
    }
}
