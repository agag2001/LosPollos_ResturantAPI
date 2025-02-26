using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosPollos.Infrastructrue.Migrations
{
    /// <inheritdoc />
    public partial class fixNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Resturants_ResturantId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resturants_AspNetUsers_OwnerId",
                table: "Resturants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resturants",
                table: "Resturants");

            migrationBuilder.RenameTable(
                name: "Resturants",
                newName: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "CantactPhone",
                table: "Restaurants",
                newName: "ContactPhone");

            migrationBuilder.RenameColumn(
                name: "CantactEmail",
                table: "Restaurants",
                newName: "ContactEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Resturants_OwnerId",
                table: "Restaurants",
                newName: "IX_Restaurants_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurants_ResturantId",
                table: "Dishes",
                column: "ResturantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurants_ResturantId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Resturants");

            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Resturants",
                newName: "CantactPhone");

            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "Resturants",
                newName: "CantactEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_OwnerId",
                table: "Resturants",
                newName: "IX_Resturants_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resturants",
                table: "Resturants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Resturants_ResturantId",
                table: "Dishes",
                column: "ResturantId",
                principalTable: "Resturants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resturants_AspNetUsers_OwnerId",
                table: "Resturants",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
