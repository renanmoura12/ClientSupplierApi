using ClientSupplierApi.Validator;
using System.ComponentModel.DataAnnotations;

namespace ClientSupplierApi.Models
{
    public class Customer_supplier_address
    {
        [Key]
        public int Id { get; set; }
        public int CustomerSupplierId { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string? Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public Customer_supplier CustomerSupplier { get; set; }

        public Customer_supplier_address()
        {

        }

        public Customer_supplier_address(int customerSupplierId, string country, string address, string? complement, string city, string state, string postalCode)
        {
            ValidateDomain(customerSupplierId, country, address, city, state, postalCode);
            Complement = complement;
        }

        private void ValidateDomain(int customerSupplierId, string country, string address, string city, string state, string postalCode)
        {
            DomainExceptionValidator.When(customerSupplierId == 0, "customer_supplier precisa ser informado");
            DomainExceptionValidator.When(country == null, "País é obrigatorio");
            DomainExceptionValidator.When(address == null, "Endereço é obrigatorio");
            DomainExceptionValidator.When(city == null, "Cidade é obrigatorio");
            DomainExceptionValidator.When(state == null, "Estado é obrigatorio");
            DomainExceptionValidator.When(postalCode.Length > 10, "Código postal é invalido");

            CustomerSupplierId = customerSupplierId;
            Country = country;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
        }
    }
}
