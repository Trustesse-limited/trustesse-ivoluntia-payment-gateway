using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Trustesse.Ivoluntia.Payment.Gateway.Models;

namespace Trustesse.Ivoluntia.Payment.Gateway.Data.Context
{
    public class PaymentDataContext : DbContext
    {
        public PaymentDataContext(DbContextOptions<PaymentDataContext> Options) : base(Options)
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
                    PaymentRequestId = "pay001",
                    Initiatorid = "user101",
                    UserEmail = "testuser1@example.com",
                    Amount = "5000",
                    Status = "initialize",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    ServiceProvider = "paystack",
                    ProgramId = "prog01",
                    ProgramType = "Scholarship",
                    ServiceProviderReference = "psrefabc123"
                },
                new PaymentRequestEntity
                {
                    PaymentRequestId = "pay002",
                    Initiatorid = "user102",
                    UserEmail = "testuser2@example.com",
                    Amount = "7500",
                    Status = "initialize",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    ServiceProvider = "paystack",
                    ProgramId = "prog02",
                    ProgramType = "Donation",
                    ServiceProviderReference = "fwrefdef456"
                }
            );
        }
    }
}
