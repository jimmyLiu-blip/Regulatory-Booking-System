using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFScheduling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_RegulationTestItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegulationTestItems",
                columns: table => new
                {
                    RegulationTestItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegulationId = table.Column<int>(type: "int", nullable: false),
                    TestItemId = table.Column<int>(type: "int", nullable: false),
                    RequirementType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationTestItems", x => x.RegulationTestItemId);
                    table.ForeignKey(
                        name: "FK_RegulationTestItems_Regulations_RegulationId",
                        column: x => x.RegulationId,
                        principalTable: "Regulations",
                        principalColumn: "RegulationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegulationTestItems_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "TestItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegulationTestItems_RegulationId",
                table: "RegulationTestItems",
                column: "RegulationId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulationTestItems_TestItemId",
                table: "RegulationTestItems",
                column: "TestItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegulationTestItems");
        }
    }
}
