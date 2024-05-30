using ClientSupplierApi.Validator;
using System.ComponentModel.DataAnnotations;

namespace ClientSupplierApi.Models
{
    public class Customer_supplier_contact
    {
        [Key]
        public int Id { get; set; }
        public int CustomerSupplierId { get; set; }
        public string Phone { get; private set; }

        [EmailAddress(ErrorMessage = "Forneça um email válido")]
        public string Email { get; private set; }

        public Customer_supplier CustomerSupplier { get; set; }

        public Customer_supplier_contact()
        {
                
        }

        public Customer_supplier_contact( int customerSupplierId, string phone, string email)
        {
            ValidateDomain(customerSupplierId, phone, email);
        }

        private void ValidateDomain(int customerSupplierId, string phone, string email)
        {
            DomainExceptionValidator.When(customerSupplierId == 0, "customer_supplier precisa ser informado");
            DomainExceptionValidator.When(phone == null, "Telefone é obrigatorio");
            DomainExceptionValidator.When(email == null, "E-mail é obrigatorio");

            CustomerSupplierId = customerSupplierId;
            Phone = phone;
            Email = email;
        }
    }
}
