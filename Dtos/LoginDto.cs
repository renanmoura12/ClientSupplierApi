using System.ComponentModel.DataAnnotations;

namespace ClientSupplierApi.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O nome é obrigatorio")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Senha é obrigatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
