using ClientSupplierApi.Enums;

namespace ClientSupplierApi.Dtos
{
    public class CustomerSupplierDto
    {
        public string Name { get; set; }
        public CustomerSupplierEnum Type { get; set; }
        public string CpfCnpj { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string? Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}
