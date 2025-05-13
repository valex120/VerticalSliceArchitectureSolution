using CompanyManagementApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var databaseProvider = configuration.GetValue<string>("DatabaseProvider");

// Настройка EF Core на использование нужного провайдера
if (databaseProvider == "PostgreSQL")
{
    var connectionString = configuration.GetConnectionString("PostgreSQL");
    builder.Services.AddDbContext<CompanyDbContext>(options => options.UseNpgsql(connectionString));
}
else
{
    // По умолчанию используется SQLite
    var connectionString = configuration.GetConnectionString("SQLite");
    builder.Services.AddDbContext<CompanyDbContext>(options => options.UseSqlite(connectionString));
}

builder.Services.AddRazorPages();

var app = builder.Build();
app.UseStaticFiles(); 

// Применение миграций при запуске
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Companies/Index");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();