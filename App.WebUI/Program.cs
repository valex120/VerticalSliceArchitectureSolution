using Microsoft.EntityFrameworkCore;
using App.Infrastructure.Data;
using App.Features.Companies.Create;
using App.Features.Companies.Edit;
using App.Features.Companies.Delete;
using App.Features.Companies.GetList;


var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Database connection string not found!");

// Registration `AppDbContext` for clear migration from App.WebUI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("App.Infrastructure")));

builder.Services.AddTransient<CreateCompanyCommandHandler>();
builder.Services.AddTransient<EditCompanyCommandHandler>();
builder.Services.AddTransient<DeleteCompanyCommandHandler>();
builder.Services.AddTransient<GetCompaniesQueryHandler>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Companies}/{action=Index}/{id?}");

app.Run();