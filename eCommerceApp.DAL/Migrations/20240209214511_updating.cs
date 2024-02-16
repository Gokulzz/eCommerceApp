using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_cartID",
                table: "Carts");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_userId",
                table: "Carts",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_userId",
                table: "Carts",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_userId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_userId",
                table: "Carts");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_cartID",
                table: "Carts",
                column: "cartID",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
