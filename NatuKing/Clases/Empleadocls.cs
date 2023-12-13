using System.ComponentModel.DataAnnotations;
namespace NatuKing.Clases
{
    public class Empleadocls
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Puesto { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Horario { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public float Sueldo { get; set; }
        [RegularExpression(@"^([0-9]{3})([0-9]{3})([0-9]{4})$", ErrorMessage = "Not a valid phone number")]//Valida con expresion Regular
        public String Telefono { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        [EmailAddress(ErrorMessage = "Correo Invalido")]
        public String Correo { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Password { get; set;}
        public int Habilitado { get; set;}
        public int Id { get;set;}
    }
}
