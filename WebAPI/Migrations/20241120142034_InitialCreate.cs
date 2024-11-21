using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Envi",
                columns: table => new
                {
                    Environmentid = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalEnergy = table.Column<double>(type: "REAL", nullable: false),
                    CrudeFuel = table.Column<double>(type: "REAL", nullable: false),
                    GasFuel = table.Column<double>(type: "REAL", nullable: false),
                    PurchElec = table.Column<double>(type: "REAL", nullable: false),
                    Year = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envi", x => x.Environmentid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Envi");
        }
    }
}
