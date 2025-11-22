using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamMan.Api.Events.Migrations
{
    /// <inheritdoc />
    public partial class addevents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ActionableEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ActionableEvents",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "ActionableEvents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ActionableEvents",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ActionableEvents",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecurrenceRules",
                table: "ActionableEvents",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ActionableEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "EventOccurrences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionableEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduledTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ExecutedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    LastError = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventOccurrences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventOccurrences_ActionableEvents_ActionableEventId",
                        column: x => x.ActionableEventId,
                        principalTable: "ActionableEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionableEvents_EventType",
                table: "ActionableEvents",
                column: "EventType");

            migrationBuilder.CreateIndex(
                name: "IX_ActionableEvents_IsActive",
                table: "ActionableEvents",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_EventOccurrences_ActionableEventId",
                table: "EventOccurrences",
                column: "ActionableEventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventOccurrences_ActionableEventId_ScheduledTime",
                table: "EventOccurrences",
                columns: new[] { "ActionableEventId", "ScheduledTime" });

            migrationBuilder.CreateIndex(
                name: "IX_EventOccurrences_ScheduledTime",
                table: "EventOccurrences",
                column: "ScheduledTime");

            migrationBuilder.CreateIndex(
                name: "IX_EventOccurrences_Status",
                table: "EventOccurrences",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_EventOccurrences_Status_ScheduledTime",
                table: "EventOccurrences",
                columns: new[] { "Status", "ScheduledTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventOccurrences");

            migrationBuilder.DropIndex(
                name: "IX_ActionableEvents_EventType",
                table: "ActionableEvents");

            migrationBuilder.DropIndex(
                name: "IX_ActionableEvents_IsActive",
                table: "ActionableEvents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ActionableEvents");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ActionableEvents");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "ActionableEvents");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ActionableEvents");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ActionableEvents");

            migrationBuilder.DropColumn(
                name: "RecurrenceRules",
                table: "ActionableEvents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ActionableEvents");
        }
    }
}
