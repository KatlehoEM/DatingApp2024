using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class VisitEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    VisitorId = table.Column<int>(type: "INTEGER", nullable: false),
                    VisitedId = table.Column<int>(type: "INTEGER", nullable: false),
                    VisitedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => new { x.VisitorId, x.VisitedId });
                    table.ForeignKey(
                        name: "FK_Visits_AspNetUsers_VisitedId",
                        column: x => x.VisitedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_AspNetUsers_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitedId",
                table: "Visits",
                column: "VisitedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");
        }
    }
}
