using ClientSupplierApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientSupplierApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Customer_supplier> Customer_Supplier { get; set; }
        public DbSet<Customer_supplier_address> Customer_supplier_address { get; set; }
        public DbSet<Customer_supplier_contact> Customer_supplier_contact { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
