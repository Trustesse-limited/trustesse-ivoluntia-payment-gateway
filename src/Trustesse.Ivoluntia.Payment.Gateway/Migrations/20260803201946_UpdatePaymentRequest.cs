using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trustesse.Ivoluntia.Payment.Gateway.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentRequestId",
                table: "PaymentRequests",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PaymentRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeprecated",
                table: "PaymentRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "PaymentRequests",
                keyColumn: "Id",
                keyValue: "pay001",
                columns: new[] { "CreatedBy", "DateCreated", "DateUpdated", "IsDeprecated" },
                values: new object[] { null, new DateTime(2026, 8, 3, 20, 19, 46, 113, DateTimeKind.Utc).AddTicks(1256), new DateTime(2026, 8, 3, 20, 19, 46, 113, DateTimeKind.Utc).AddTicks(1257), false });

            migrationBuilder.UpdateData(
                table: "PaymentRequests",
                keyColumn: "Id",
                keyValue: "pay002",
                columns: new[] { "CreatedBy", "DateCreated", "DateUpdated", "IsDeprecated" },
                values: new object[] { null, new DateTime(2026, 8, 3, 20, 19, 46, 113, DateTimeKind.Utc).AddTicks(1264), new DateTime(2026, 8, 3, 20, 19, 46, 113, DateTimeKind.Utc).AddTicks(1265), false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "IsDeprecated",
                table: "PaymentRequests");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentRequests",
                newName: "PaymentRequestId");

            migrationBuilder.UpdateData(
                table: "PaymentRequests",
                keyColumn: "PaymentRequestId",
                keyValue: "pay001",
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 2, 27, 17, 40, 4, 7, DateTimeKind.Utc).AddTicks(9626), new DateTime(2026, 2, 27, 17, 40, 4, 7, DateTimeKind.Utc).AddTicks(9626) });

            migrationBuilder.UpdateData(
                table: "PaymentRequests",
                keyColumn: "PaymentRequestId",
                keyValue: "pay002",
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2026, 2, 27, 17, 40, 4, 7, DateTimeKind.Utc).AddTicks(9630), new DateTime(2026, 2, 27, 17, 40, 4, 7, DateTimeKind.Utc).AddTicks(9631) });
        }
    }
}
