using System.ComponentModel.DataAnnotations;

namespace asp_net_labs_3.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [RegularExpression(pattern: @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "E-mail введен некорректно")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
