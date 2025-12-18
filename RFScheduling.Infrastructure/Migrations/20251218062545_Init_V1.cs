using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFScheduling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Regulations_RegulationId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_TestItems_TestItemId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Users_EngineerId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "ProjectTestRecords");

            migrationBuilder.DropIndex(
                name: "IX_Projects_RegulationId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TestItemId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EstimateEnd",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "RegulationName",
                table: "Regulations");

            migrationBuilder.DropColumn(
                name: "RegulationId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TestItemId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "EngineerId",
                table: "Schedules",
                newName: "ProjectTestItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_EngineerId",
                table: "Schedules",
                newName: "IX_Schedules_ProjectTestItemId");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TestLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsModifiable",
                table: "TestLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OriginalEstimatedStart",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OriginalEstimatedEnd",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "Schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimatedStart",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedEnd",
                table: "Schedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScheduleType",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OldStart",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OldEnd",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NewStart",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NewEnd",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "EstimateHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ProgressReports",
                columns: table => new
                {
                    ProgressReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    ReportStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportedBy = table.Column<int>(type: "int", nullable: false),
                    ReportedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressReports", x => x.ProgressReportId);
                    table.ForeignKey(
                        name: "FK_ProgressReports_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgressReports_Users_ReportedBy",
                        column: x => x.ReportedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRegulations",
                columns: table => new
                {
                    ProjectRegulationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    RegulationId = table.Column<int>(type: "int", nullable: true),
                    OtherRegulationText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRegulations", x => x.ProjectRegulationId);
                    table.ForeignKey(
                        name: "FK_ProjectRegulations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRegulations_Regulations_RegulationId",
                        column: x => x.RegulationId,
                        principalTable: "Regulations",
                        principalColumn: "RegulationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTestItems",
                columns: table => new
                {
                    ProjectTestItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TestItemId = table.Column<int>(type: "int", nullable: true),
                    OtherTestItemText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestItemType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTestItems", x => x.ProjectTestItemId);
                    table.ForeignKey(
                        name: "FK_ProjectTestItems_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTestItems_TestItems_TestItemId",
                        column: x => x.TestItemId,
                        principalTable: "TestItems",
                        principalColumn: "TestItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceEngineers",
                columns: table => new
                {
                    ResourceEngineerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    EngineerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceEngineers", x => x.ResourceEngineerId);
                    table.ForeignKey(
                        name: "FK_ResourceEngineers_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourceEngineers_Users_EngineerId",
                        column: x => x.EngineerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleEngineers",
                columns: table => new
                {
                    ScheduleEngineerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    EngineerId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEngineers", x => x.ScheduleEngineerId);
                    table.ForeignKey(
                        name: "FK_ScheduleEngineers_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleEngineers_Users_EngineerId",
                        column: x => x.EngineerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActualTestRecords",
                columns: table => new
                {
                    ActualTestRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTestItemId = table.Column<int>(type: "int", nullable: false),
                    ActualStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalDuration = table.Column<int>(type: "int", nullable: true),
                    PauseCount = table.Column<int>(type: "int", nullable: false),
                    LastCalculatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActualTestRecords", x => x.ActualTestRecordId);
                    table.ForeignKey(
                        name: "FK_ActualTestRecords_ProjectTestItems_ProjectTestItemId",
                        column: x => x.ProjectTestItemId,
                        principalTable: "ProjectTestItems",
                        principalColumn: "ProjectTestItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewRecords",
                columns: table => new
                {
                    ReviewRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectTestItemId = table.Column<int>(type: "int", nullable: false),
                    ReviewRound = table.Column<int>(type: "int", nullable: false),
                    ReviewResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedBy = table.Column<int>(type: "int", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRecords", x => x.ReviewRecordId);
                    table.ForeignKey(
                        name: "FK_ReviewRecords_ProjectTestItems_ProjectTestItemId",
                        column: x => x.ProjectTestItemId,
                        principalTable: "ProjectTestItems",
                        principalColumn: "ProjectTestItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewRecords_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewRecords_Users_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActualTestRecords_ProjectTestItemId",
                table: "ActualTestRecords",
                column: "ProjectTestItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReports_ReportedBy",
                table: "ProgressReports",
                column: "ReportedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressReports_ScheduleId",
                table: "ProgressReports",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRegulations_ProjectId",
                table: "ProjectRegulations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRegulations_RegulationId",
                table: "ProjectRegulations",
                column: "RegulationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTestItems_ProjectId",
                table: "ProjectTestItems",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTestItems_TestItemId",
                table: "ProjectTestItems",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceEngineers_EngineerId",
                table: "ResourceEngineers",
                column: "EngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceEngineers_ResourceId",
                table: "ResourceEngineers",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRecords_ProjectId",
                table: "ReviewRecords",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRecords_ProjectTestItemId",
                table: "ReviewRecords",
                column: "ProjectTestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRecords_ReviewedBy",
                table: "ReviewRecords",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEngineers_EngineerId",
                table: "ScheduleEngineers",
                column: "EngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEngineers_ScheduleId",
                table: "ScheduleEngineers",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ProjectTestItems_ProjectTestItemId",
                table: "Schedules",
                column: "ProjectTestItemId",
                principalTable: "ProjectTestItems",
                principalColumn: "ProjectTestItemId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ProjectTestItems_ProjectTestItemId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "ActualTestRecords");

            migrationBuilder.DropTable(
                name: "ProgressReports");

            migrationBuilder.DropTable(
                name: "ProjectRegulations");

            migrationBuilder.DropTable(
                name: "ResourceEngineers");

            migrationBuilder.DropTable(
                name: "ReviewRecords");

            migrationBuilder.DropTable(
                name: "ScheduleEngineers");

            migrationBuilder.DropTable(
                name: "ProjectTestItems");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TestLogs");

            migrationBuilder.DropColumn(
                name: "IsModifiable",
                table: "TestLogs");

            migrationBuilder.DropColumn(
                name: "EstimatedEnd",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ScheduleType",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ProjectTestItemId",
                table: "Schedules",
                newName: "EngineerId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_ProjectTestItemId",
                table: "Schedules",
                newName: "IX_Schedules_EngineerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OriginalEstimatedStart",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OriginalEstimatedEnd",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimatedStart",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimateEnd",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RegulationName",
                table: "Regulations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegulationId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TestItemId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OldStart",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OldEnd",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NewStart",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NewEnd",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "EstimateHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "EstimateHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectTestRecords",
                columns: table => new
                {
                    ProjectTestRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActualEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDuration = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTestRecords", x => x.ProjectTestRecordId);
                    table.ForeignKey(
                        name: "FK_ProjectTestRecords_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RegulationId",
                table: "Projects",
                column: "RegulationId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TestItemId",
                table: "Projects",
                column: "TestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTestRecords_ProjectId",
                table: "ProjectTestRecords",
                column: "ProjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Regulations_RegulationId",
                table: "Projects",
                column: "RegulationId",
                principalTable: "Regulations",
                principalColumn: "RegulationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_TestItems_TestItemId",
                table: "Projects",
                column: "TestItemId",
                principalTable: "TestItems",
                principalColumn: "TestItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Users_EngineerId",
                table: "Schedules",
                column: "EngineerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
