using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Models;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shop.Dapper;
using System.Reflection;
using NLog;
using System.Configuration;
using NLog.Web;

//var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie("Cookies", options => {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddSingleton<IDbConnection>(sp => {
    return new SqlConnection(builder.Configuration.GetConnectionString("DBConnection"));
});
builder.Services.AddTransient<IBaseRepository, BaseRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Host.UseNLog();
LogManager.Configuration.Variables["connectionString"] = builder.Configuration.GetConnectionString("NLog");

// make reflection about auto DI
//var types = Assembly.GetExecutingAssembly().GetTypes()
//    .Where(t => t.IsClass && !t.IsAbstract)
//    .Select(t => new {
//        Service = t.GetInterfaces().FirstOrDefault(),
//        Implementation = t
//    })
//    .Where(x => x.Service != null);

//foreach (var type in types) {
//    if (type != null) builder.Services.AddScoped(type.Service, type.Implementation);
//}

//IEnumerable<Type> interfaceTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsInterface);
//foreach (var interfaceType in interfaceTypes) {
//    IServiceCollection serviceCollection = builder.Services.AddTransient(interfaceType, Activator.CreateInstance(interfaceType));
//}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapGet("/", () => {
//    LogManager.GetCurrentClassLogger().Info("Hello World!");
//    return "Hello World!";
//});
app.Run();

