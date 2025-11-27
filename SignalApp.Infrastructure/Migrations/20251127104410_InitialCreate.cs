using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Signals",
                columns: table => new
                {
                    SignalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SignalType = table.Column<int>(type: "INTEGER", nullable: false),
                    Amplitude = table.Column<double>(type: "REAL", nullable: false),
                    Frequency = table.Column<double>(type: "REAL", nullable: false),
                    PointsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signals", x => x.SignalId);
                });

            migrationBuilder.CreateTable(
                name: "SignalPoints",
                columns: table => new
                {
                    SignalPointId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<double>(type: "REAL", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    SignalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalPoints", x => x.SignalPointId);
                    table.ForeignKey(
                        name: "FK_SignalPoints_Signals_SignalId",
                        column: x => x.SignalId,
                        principalTable: "Signals",
                        principalColumn: "SignalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignalPoints_SignalId",
                table: "SignalPoints",
                column: "SignalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignalPoints");

            migrationBuilder.DropTable(
                name: "Signals");
        }
    }
}
