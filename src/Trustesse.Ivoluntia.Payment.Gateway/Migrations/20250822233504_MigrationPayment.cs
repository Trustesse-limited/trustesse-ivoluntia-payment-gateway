using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trustesse.Ivoluntia.Payment.Gateway.Migrations
{
    /// <inheritdoc />
    public partial class MigrationPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                columns: table => new
                {
                    PaymentRequestId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Initiatorid = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserEmail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ServiceProvider = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProgramId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProgramType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServiceProviderReference = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.PaymentRequestId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "PaymentRequests",
                columns: new[] { "PaymentRequestId", "Amount", "DateCreated", "DateUpdated", "Initiatorid", "ProgramId", "ProgramType", "ServiceProvider", "ServiceProviderReference", "Status", "UserEmail" },
                values: new object[,]
                {
                    { "pay001", "5000", new DateTime(2025, 8, 22, 23, 35, 4, 327, DateTimeKind.Utc).AddTicks(5854), new DateTime(2025, 8, 22, 23, 35, 4, 327, DateTimeKind.Utc).AddTicks(5856), "user101", "prog01", "Scholarship", "Paystack", "psrefabc123", "initialize", "testuser1@example.com" },
                    { "pay002", "7500", new DateTime(2025, 8, 22, 23, 35, 4, 327, DateTimeKind.Utc).AddTicks(5859), new DateTime(2025, 8, 22, 23, 35, 4, 327, DateTimeKind.Utc).AddTicks(5860), "user102", "prog02", "Donation", "Paystack", "fwrefdef456", "initialize", "testuser2@example.com" }
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
