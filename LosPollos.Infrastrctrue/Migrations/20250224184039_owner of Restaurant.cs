using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosPollos.Infrastructrue.Migrations
{
    /// <inheritdoc />
    public partial class ownerofRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Resturants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
            migrationBuilder.Sql("Update Resturants Set ownerId = (select top 1 Id from AspNetUsers where Email = 'owner@agag.com')");
            migrationBuilder.CreateIndex(
                name: "IX_Resturants_OwnerId",
                table: "Resturants",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resturants_AspNetUsers_OwnerId",
                table: "Resturants",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resturants_AspNetUsers_OwnerId",
                table: "Resturants");

            migrationBuilder.DropIndex(
                name: "IX_Resturants_OwnerId",
                table: "Resturants");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Resturants");
        }
    }
}
