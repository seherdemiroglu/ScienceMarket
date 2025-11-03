using System.ComponentModel.DataAnnotations;

namespace ScienceMarket.Models;

public class LoginViewModel
{
    [Display(Name = "E-Posta")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
    public string? UserName { get; set; }

    [Display(Name = "Parola")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Bu alan boş bırakılamaz")] 
    public string? Password { get; set; }


    [Display(Name = "Oturum Açık Kalsın")]
    public bool IsPersistent { get; set; } = true;
    public string? ReturnUrl { get; set; }
}
