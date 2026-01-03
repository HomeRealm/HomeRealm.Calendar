using FamMan.Api.Calendars;
using FamMan.Api.Calendars.Endpoints;
using FamMan.Api.Calendars.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCalendarServices();

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<CalendarDbContext>("famman-calendar");
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();


// automatically apply migrations during startup
// will be moved to background service in future
using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<CalendarDbContext>();
  dbContext.Database.Migrate();
}

app.MapDefaultEndpoints();
var api = app.MapGroup("/api");
api.MapCalendarsEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();