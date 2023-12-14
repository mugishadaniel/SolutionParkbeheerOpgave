using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parken",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Huizen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Straat = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Nr = table.Column<int>(type: "int", nullable: false),
                    Actief = table.Column<bool>(type: "bit", nullable: false),
                    ParkId = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huizen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huizen_Parken_ParkId",
                        column: x => x.ParkId,
                        principalTable: "Parken",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Huurders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 25, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HuisEFId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huurders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huurders_Huizen_HuisEFId",
                        column: x => x.HuisEFId,
                        principalTable: "Huizen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Huurcontracten",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aantaldagen = table.Column<int>(type: "int", nullable: false),
                    HuurderId = table.Column<int>(type: "int", nullable: true),
                    HuisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huurcontracten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huurcontracten_Huizen_HuisId",
                        column: x => x.HuisId,
                        principalTable: "Huizen",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Huurcontracten_Huurders_HuurderId",
                        column: x => x.HuurderId,
                        principalTable: "Huurders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Huizen_ParkId",
                table: "Huizen",
                column: "ParkId");

            migrationBuilder.CreateIndex(
                name: "IX_Huurcontracten_HuisId",
                table: "Huurcontracten",
                column: "HuisId");

            migrationBuilder.CreateIndex(
                name: "IX_Huurcontracten_HuurderId",
                table: "Huurcontracten",
                column: "HuurderId");

            migrationBuilder.CreateIndex(
                name: "IX_Huurders_HuisEFId",
                table: "Huurders",
                column: "HuisEFId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Huurcontracten");

            migrationBuilder.DropTable(
                name: "Huurders");

            migrationBuilder.DropTable(
                name: "Huizen");

            migrationBuilder.DropTable(
                name: "Parken");
        }
    }
}
