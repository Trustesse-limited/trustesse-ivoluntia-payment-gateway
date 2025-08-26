using Microsoft.EntityFrameworkCore;
using Trustesse.Ivoluntia.Payment.Gateway.Context;
using Trustesse.Ivoluntia.Payment.Gateway.IRepository;
using Trustesse.Ivoluntia.Payment.Gateway.IService;
using Trustesse.Ivoluntia.Payment.Gateway.Repository;
using Trustesse.Ivoluntia.Payment.Gateway.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
//
builder.Services.AddDbContext<PaymentDataContext>(option =>
option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
//
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddLogging();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
