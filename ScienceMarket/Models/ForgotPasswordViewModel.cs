using System.ComponentModel.DataAnnotations;

namespace ScienceMarket.Models;

public class ForgotPasswordViewModel
{
    [Display(Name = "E-Posta")]
    [Required(ErrorMessage = "{0} Alanı boş bırakılamaz")]
    public string UserName { get; set; }
}
