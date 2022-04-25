using Microsoft.EntityFrameworkCore;
using WebVendas.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//string _devConnectionString = builder.Configuration.GetConnectionString("DevConnection");
string _prodConnectionString = builder.Configuration.GetConnectionString("ProdConnection");

// Dependency Injection for Entity Framework - Development
/*builder.Services.AddDbContext<Context>(options => options.UseMySql(
    _prodConnectionString,
    ServerVersion.AutoDetect(_devConnectionString)
    )
);*/

// Dependency Injection for Entity Framework - Production
builder.Services.AddDbContext<Context>(options => options.UseMySql(
    _prodConnectionString,
    ServerVersion.AutoDetect(_prodConnectionString)
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sale}/{action=Index}/{id?}");

app.Run();
