using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trustesse.Ivoluntia.Payment.Gateway.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                columns: table => new
                {
                    PaymentRequestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Initiatorid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePaidFor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.PaymentRequestId);
                });

            migrationBuilder.InsertData(
                table: "PaymentRequests",
                columns: new[] { "PaymentRequestId", "Amount", "DateCreated", "DateUpdated", "Initiatorid", "Reference", "ServiceId", "ServicePaidFor", "ServiceProvider", "Status", "UserEmail" },
                values: new object[,]
                {
                    { "pay001", 5000m, new DateTime(2026, 2, 27, 17, 40, 4, 7, DateTimeKind.Utc).AddTicks(9626), new DateTime(2026, 2, 27, 17, 40, 4, 7, DateTimeKind.Utc).AddTicks(9626), "user101", "psrefabc123", "prog01", "Scholarship", "paystack", "initialize", "testuser1@example.com" },
                    { "pay002", 7500m, new DateTime(2026, 2, 27, 17, 40, 4, 7, DateTimeKind.Utc).AddTicks(9630), new DateTime(2026, 2, 27, 17, 40, 4, 7, DateTimeKind.Utc).AddTicks(9631), "user102", "fwrefdef456", "prog02", "Donation", "paystack", "initialize", "testuser2@example.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRequests");
        }
    }
}
