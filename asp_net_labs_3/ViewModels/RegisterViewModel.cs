using System;
using System.ComponentModel.DataAnnotations;

namespace asp_net_labs_3.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Укажите фамилию")]
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите эл.почту")]
        [RegularExpression(pattern: @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "E-mail введен некорректно")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        [RegularExpression(pattern: @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Номер телефона введен некорректно")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Используйте не менее 8 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
