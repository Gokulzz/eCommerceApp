using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class gokul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Payment",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "orderId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Payment_orderId",
                table: "Payment",
                column: "orderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Orders_orderId",
                table: "Payment",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "orderId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Orders_orderId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_orderId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "Payment");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Payment",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
