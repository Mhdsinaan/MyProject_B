using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_users_UserId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_UserId",
                table: "CartProducts");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "CartProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_UsersId",
                table: "CartProducts",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_users_UsersId",
                table: "CartProducts",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_users_UsersId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_UsersId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "CartProducts");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_UserId",
                table: "CartProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_users_UserId",
                table: "CartProducts",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
