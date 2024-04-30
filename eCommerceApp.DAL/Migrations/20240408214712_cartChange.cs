using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class cartChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cartCheckout",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cartCheckout",
                table: "Carts");
        }
    }
}
