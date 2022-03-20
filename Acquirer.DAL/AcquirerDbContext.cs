using Acquirer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Acquirer.DAL
{
    public class AcquirerDbContext : DbContext
    {
        public DbSet<PaymentMethodEntity> PaymentMethods { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }

        public AcquirerDbContext(DbContextOptions<AcquirerDbContext> options)
            : base(options) { }
    }

}