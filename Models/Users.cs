using ClientSupplierApi.Validator;
using System.ComponentModel.DataAnnotations;

namespace ClientSupplierApi.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; private set; }

        [EmailAddress(ErrorMessage = "Forneça um email válido")]
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public Users() { }

        public Users(string name, string email)
        {
            ValidateDomain(name, email);
        }

        public Users(int id, string name, string email)
        {
            DomainExceptionValidator.When(id < 0, "O id não pode ser negativo");
            Id = id;
            ValidateDomain(name, email);
        }

        public void AlterarSenha(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        private void ValidateDomain(string userName, string email)
        {
            DomainExceptionValidator.When(userName == null, "O nome é obrigatório");
            DomainExceptionValidator.When(email == null, "O email é obrigatório");
            DomainExceptionValidator.When(userName.Length > 250, "O nome não pode ultrapassar os 250 caracteres");
            DomainExceptionValidator.When(email.Length > 200, "O email não pode ultrapassar os 200 caracteres");
            UserName = userName;
            Email = email;
        }

    }
}
