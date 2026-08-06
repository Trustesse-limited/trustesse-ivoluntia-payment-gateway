using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Entity;
using static System.Net.WebRequestMethods;

namespace Trustesse.Ivoluntia.Payment.Gateway.Data.Context
{
    public class iVoluntiaDataContext : DbContext
    {
        public iVoluntiaDataContext(DbContextOptions<iVoluntiaDataContext> Options) : base(Options)
        {
        }
        public DbSet<PaymentRequestEntity> PaymentRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PaymentRequestEntity>().HasData
            (
                new PaymentRequestEntity
                {
                    Id = "pay001",
                    Initiatorid = "user101",
                    UserEmail = "testuser1@example.com",
                    Amount = 5000,
                    Status = "initialize",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    ServiceProvider = "paystack",
                    ServiceId = "prog01",
                    ServicePaidFor = "Scholarship",
                    Reference = "psrefabc123"
                },
                new PaymentRequestEntity
                {
                    Id = "pay002",
                    Initiatorid = "user102",
                    UserEmail = "testuser2@example.com",
                    Amount = 7500,
                    Status = "initialize",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    ServiceProvider = "paystack",
                    ServiceId = "prog02",
                    ServicePaidFor = "Donation",
                    Reference = "fwrefdef456"
                }
            );
        }
    }
}
