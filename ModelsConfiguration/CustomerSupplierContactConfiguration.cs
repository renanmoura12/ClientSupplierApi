using ClientSupplierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSupplierApi.ModelsConfiguration
{
    public class CustomerSupplierContactConfiguration : IEntityTypeConfiguration<Customer_supplier_contact>
    {
        public void Configure(EntityTypeBuilder<Customer_supplier_contact> builder)
        {
            builder.Property(a => a.Email).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Phone).HasMaxLength(15).IsRequired();
        }
    }
}
