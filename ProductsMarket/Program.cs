using Microsoft.EntityFrameworkCore;
using ProductsMarket.Data;
using ProductsMarket.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ������� ��������� ���������� �� ���� �����
builder.Services.AddScoped<DBConnection>(); // �������� DBConnection � ����� ���������

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var dbConnection = serviceProvider.GetService<DBConnection>();
    var connectionString = dbConnection.GetConnectionString();
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    options.UseMySql(connectionString, serverVersion);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tables}/{action=Index}/{id?}");

app.Run();
