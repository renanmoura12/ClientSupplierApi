using ClientSupplierApi.Enums;
using ClientSupplierApi.Validator;
using System.ComponentModel.DataAnnotations;

namespace ClientSupplierApi.Models
{
    public class Customer_supplier
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public CustomerSupplierEnum Type { get; set; }

        public Customer_supplier_contact Contact { get; set; }
        public Customer_supplier_address Address { get; set; }

        public Customer_supplier()
        {

        }

        public Customer_supplier(string name, CustomerSupplierEnum type, string cpfCnpj)
        {
            ValidateDomain(name, cpfCnpj);
            Type = type;
        }

        private void ValidateDomain(string name, string cpfCnpj)
        {
            DomainExceptionValidator.When(name == null, "O nome é obrigatório");
            DomainExceptionValidator.When(name.Length > 250, "O nome não pode ultrapassar os 250 caracteres");
            DomainExceptionValidator.When(cpfCnpj.Length > 14, "Este campo não pode ultrapassar os 14 caracteres");
            CpfCnpj = cpfCnpj;
            Name = name;
        }
    }
}
