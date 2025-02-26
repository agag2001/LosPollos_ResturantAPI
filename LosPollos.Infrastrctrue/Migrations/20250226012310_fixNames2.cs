using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosPollos.Infrastructrue.Migrations
{
    /// <inheritdoc />
    public partial class fixNames2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Restaurants",
                newName: "ContactNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Restaurants",
                newName: "ContactPhone");
        }
    }
}
