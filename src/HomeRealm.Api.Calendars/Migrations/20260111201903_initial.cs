using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeRealm.Api.Calendars.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "Calendars",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
          Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
          Owner = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
          Visibility = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Calendars", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Categories",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          Color = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          Icon = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Categories", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "CalendarEvents",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          CalendarId = table.Column<Guid>(type: "uuid", nullable: false),
          Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
          End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
          Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          AllDay = table.Column<bool>(type: "boolean", nullable: false),
          RecurrenceId = table.Column<Guid>(type: "uuid", nullable: false),
          CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
          LinkedResource = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_CalendarEvents", x => x.Id);
          table.ForeignKey(
                      name: "FK_CalendarEvents_Calendars_CalendarId",
                      column: x => x.CalendarId,
                      principalTable: "Calendars",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
                      name: "FK_CalendarEvents_Categories_CategoryId",
                      column: x => x.CategoryId,
                      principalTable: "Categories",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
        });

    migrationBuilder.CreateTable(
        name: "Attendees",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          EventId = table.Column<Guid>(type: "uuid", nullable: false),
          UserId = table.Column<Guid>(type: "uuid", nullable: false),
          Status = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          Role = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Attendees", x => x.Id);
          table.ForeignKey(
                      name: "FK_Attendees_CalendarEvents_EventId",
                      column: x => x.EventId,
                      principalTable: "CalendarEvents",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "RecurrenceRules",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          EventId = table.Column<Guid>(type: "uuid", nullable: false),
          Rule = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          OccurrenceOverrides = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
          EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_RecurrenceRules", x => x.Id);
          table.ForeignKey(
                      name: "FK_RecurrenceRules_CalendarEvents_EventId",
                      column: x => x.EventId,
                      principalTable: "CalendarEvents",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "Reminders",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          EventId = table.Column<Guid>(type: "uuid", nullable: false),
          Method = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          TimeBefore = table.Column<int>(type: "integer", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Reminders", x => x.Id);
          table.ForeignKey(
                      name: "FK_Reminders_CalendarEvents_EventId",
                      column: x => x.EventId,
                      principalTable: "CalendarEvents",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "OccurrenceOverrides",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          RecurrenceId = table.Column<Guid>(type: "uuid", nullable: false),
          Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_OccurrenceOverrides", x => x.Id);
          table.ForeignKey(
                      name: "FK_OccurrenceOverrides_RecurrenceRules_RecurrenceId",
                      column: x => x.RecurrenceId,
                      principalTable: "RecurrenceRules",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateIndex(
        name: "IX_Attendees_EventId",
        table: "Attendees",
        column: "EventId");

    migrationBuilder.CreateIndex(
        name: "IX_CalendarEvents_CalendarId",
        table: "CalendarEvents",
        column: "CalendarId");

    migrationBuilder.CreateIndex(
        name: "IX_CalendarEvents_CategoryId",
        table: "CalendarEvents",
        column: "CategoryId");

    migrationBuilder.CreateIndex(
        name: "IX_OccurrenceOverrides_RecurrenceId",
        table: "OccurrenceOverrides",
        column: "RecurrenceId");

    migrationBuilder.CreateIndex(
        name: "IX_RecurrenceRules_EventId",
        table: "RecurrenceRules",
        column: "EventId",
        unique: true);

    migrationBuilder.CreateIndex(
        name: "IX_Reminders_EventId",
        table: "Reminders",
        column: "EventId");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "Attendees");

    migrationBuilder.DropTable(
        name: "OccurrenceOverrides");

    migrationBuilder.DropTable(
        name: "Reminders");

    migrationBuilder.DropTable(
        name: "RecurrenceRules");

    migrationBuilder.DropTable(
        name: "CalendarEvents");

    migrationBuilder.DropTable(
        name: "Calendars");

    migrationBuilder.DropTable(
        name: "Categories");
  }
}

