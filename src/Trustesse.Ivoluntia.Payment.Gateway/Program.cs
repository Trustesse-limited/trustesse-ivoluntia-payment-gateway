using Microsoft.EntityFrameworkCore;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Context;
using Trustesse.Ivoluntia.Payment.Gateway.Extension;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Implementation;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Interface;
using Trustesse.Ivoluntia.Payment.Gateway.Service;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); 

//builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1000;
}
    );
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddLogging();

ServiceConfig.AddCustomCors(builder.Services, builder.Configuration);
ServiceConfig.AddCustomServices(builder.Services);
ServiceConfig.AddCustomDatabase(builder.Services, builder.Configuration);
ServiceConfig.AddRateLimt(builder.Services);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseRateLimiter();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
