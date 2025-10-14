using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplyChainTransparency.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RM558521");

            migrationBuilder.CreateTable(
                name: "ComplianceReports",
                schema: "RM558521",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsCompliant = table.Column<bool>(type: "INTEGER", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "RM558521",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ContactInfo = table.Column<string>(type: "TEXT", nullable: false),
                    IsSustainable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFairTrade = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarbonFootprints",
                schema: "RM558521",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmissionsKgCO2e = table.Column<double>(type: "REAL", nullable: false),
                    ActivityType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarbonFootprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarbonFootprints_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "RM558521",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarbonFootprints_SupplierId",
                schema: "RM558521",
                table: "CarbonFootprints",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarbonFootprints",
                schema: "RM558521");

            migrationBuilder.DropTable(
                name: "ComplianceReports",
                schema: "RM558521");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "RM558521");
        }
    }
}
