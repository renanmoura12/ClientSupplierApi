using System.ComponentModel.DataAnnotations;

namespace ClientSupplierApi.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "O e-mail é obrigatorio")]
        [EmailAddress(ErrorMessage = "Forneça um email válido")]
        public string Email { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Senha é obrigatoria")]
        public string Password { get; set; }
    }
}
