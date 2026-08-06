using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Context;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Implementation;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Interface;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Extension
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                });
                options.AddPolicy("Filter", policyBuilder =>
                {
                    policyBuilder.WithOrigins(config.GetSection("CORS:AllowedOrigins").Value!.Split(','))
                                .WithMethods(config.GetSection("CORS:AllowedMethods").Value!.Split(','))
                                .WithHeaders(config.GetSection("CORS:AllowedHeaders").Value!.Split(','))
                                .AllowCredentials();
                });
            });
            return services;
        }
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IVerifyWebhookEvent, VerifyWebhookEvent>();
            services.AddScoped<IWebhookService, WebhookService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<PaystackService>();
            services.AddScoped<FlutterwaveService>();
            services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();
            services.AddScoped<IPaymentRequestRepository, PaymentRequestRepository>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
        public static IServiceCollection AddCustomDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<iVoluntiaDataContext>((spt, options) =>
            {
                options.UseSqlServer(
                   config.GetConnectionString("DefaultConnection")!,
                   sqlServerOptions => sqlServerOptions.MigrationsAssembly("Trustesse.Ivoluntia.Payment.Gateway"));
            });
            return services;
        }
        public static IServiceCollection AddRateLimt(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("fixed", opt =>
                {
                    opt.PermitLimit = 4;
                    opt.Window = TimeSpan.FromSeconds(12);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 2;
                });
            });
            return services;
        }
    }
}
