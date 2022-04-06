using System.ComponentModel.DataAnnotations;

namespace asp_net_labs_3.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerSurname { get; set; }

        public string CustomerPatronymic { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}


public enum DeliveryMethod
{
    [Display(Name = "Почта")]
    MAIL,
    [Display(Name = "Пунк самовывыоза")]
    COURIER,
    [Display(Name = "Курьер")]
    POI
}

public enum PaymentMethod
{
    [Display(Name = "Наличными")]
    CASH,
    [Display(Name = "Картой")]
    CREDIT_CARD
}