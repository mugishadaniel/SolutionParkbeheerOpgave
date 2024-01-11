using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class deletehuurdershuis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Huurders_Huizen_HuisEFId",
                table: "Huurders");

            migrationBuilder.DropIndex(
                name: "IX_Huurders_HuisEFId",
                table: "Huurders");

            migrationBuilder.DropColumn(
                name: "HuisEFId",
                table: "Huurders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HuisEFId",
                table: "Huurders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Huurders_HuisEFId",
                table: "Huurders",
                column: "HuisEFId");

            migrationBuilder.AddForeignKey(
                name: "FK_Huurders_Huizen_HuisEFId",
                table: "Huurders",
                column: "HuisEFId",
                principalTable: "Huizen",
                principalColumn: "Id");
        }
    }
}
