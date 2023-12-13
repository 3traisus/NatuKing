using System.ComponentModel.DataAnnotations;
namespace NatuKing.Clases
{
    public class User
    {
        [Display(Name = "Nombre de usuario")]
        public string Name { get; set; }
        [Display(Name = "Correo de usuario")]
        [Required(ErrorMessage = "Correo Invalido")]
        public string Email { get; set; }
        [Display(Name = "Password de usuario")]
        [Required(ErrorMessage = "Password Invalido")]
        public string Password { get; set; }
        [Display(Name = "Puesto de usuario")]
        public string Puesto { get; set; }
    }
}
