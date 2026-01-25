using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamMan.Api.Calendars.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRecurrenceRuleToOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecurrenceRules_EventId",
                table: "RecurrenceRules");

            migrationBuilder.CreateIndex(
                name: "IX_RecurrenceRules_EventId",
                table: "RecurrenceRules",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecurrenceRules_EventId",
                table: "RecurrenceRules");

            migrationBuilder.CreateIndex(
                name: "IX_RecurrenceRules_EventId",
                table: "RecurrenceRules",
                column: "EventId",
                unique: true);
        }
    }
}
