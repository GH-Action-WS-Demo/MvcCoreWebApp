using Microsoft.EntityFrameworkCore;
using MvcCoreWebApp.Models;
using System.Configuration;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    builder.Services.AddDbContext<AdventureWorksLt2016Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksDbContextAzure")));
}
else
{
    builder.Services.AddDbContext<AdventureWorksLt2016Context>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksDbContext")));
}
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the    D2.M3.IdentityInCloud.md 11 / 3 / 2022 10 / 14 default policy
// options.FallbackPolicy = options.DefaultPolicy;
//});

//builder.Services.AddRazorPages()
// .AddMvcOptions(options => { })
// .AddMicrosoftIdentityUI();



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

//app.UseAuthorization();

//app.UseAuthentication();

//app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
