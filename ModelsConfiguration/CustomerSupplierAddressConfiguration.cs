using ClientSupplierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSupplierApi.ModelsConfiguration
{
    public class CustomerSupplierAddressConfiguration : IEntityTypeConfiguration<Customer_supplier_address>
    {
        public void Configure(EntityTypeBuilder<Customer_supplier_address> builder)
        {
            builder.Property(a => a.Address).HasMaxLength(120).IsRequired();
            builder.Property(a => a.PostalCode).HasMaxLength(10).IsRequired();
            builder.Property(a => a.State).HasMaxLength(50).IsRequired();
            builder.Property(a => a.City).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Country).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Complement).HasMaxLength(50);
        }
    }
}
