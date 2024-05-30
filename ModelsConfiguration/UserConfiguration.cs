using ClientSupplierApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSupplierApi.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.Property(a => a.Email).HasMaxLength(200).IsRequired();    
            builder.Property(a => a.UserName).HasMaxLength(50).IsRequired();
        }
    }
}
