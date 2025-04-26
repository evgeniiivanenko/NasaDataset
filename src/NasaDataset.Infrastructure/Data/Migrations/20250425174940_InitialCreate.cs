using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NasaDataset.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meteorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameType = table.Column<string>(type: "text", nullable: false),
                    Recclass = table.Column<string>(type: "text", nullable: false),
                    Mass = table.Column<decimal>(type: "numeric", nullable: false),
                    Fall = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: true),
                    Reclat = table.Column<decimal>(type: "numeric", nullable: false),
                    Reclong = table.Column<decimal>(type: "numeric", nullable: false),
                    Geolocation = table.Column<string>(type: "jsonb", nullable: true),
                    ComputedRegionCbhkFwbd = table.Column<string>(type: "text", nullable: true),
                    ComputedRegionNnqa25f4 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meteorites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meteorites_ExternalId",
                table: "Meteorites",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meteorites_Year",
                table: "Meteorites",
                column: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meteorites");
        }
    }
}
