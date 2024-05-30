using ClientSupplierApi.Enums;
using ClientSupplierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSupplierApi.ModelsConfiguration
{
    public class CustomerSupplierConfiguration : IEntityTypeConfiguration<Customer_supplier>
    {
        public void Configure(EntityTypeBuilder<Customer_supplier> builder)
        {

            builder.Property(a => a.Name).HasMaxLength(250).IsRequired();
            builder.Property(a => a.Type)
                .HasConversion(a => a.ToString(), a => (CustomerSupplierEnum)Enum.Parse(typeof(CustomerSupplierEnum), a))
                .HasMaxLength(20).IsRequired();
            builder.Property(a => a.CpfCnpj).HasMaxLength(14);
        }
    }
}
