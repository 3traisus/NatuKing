using System.ComponentModel.DataAnnotations;

namespace NatuKing.Clases
{
    public class Provedorcls
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Ciudad { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Cp { get; set; }
        [RegularExpression(@"^([0-9]{3})([0-9]{3})([0-9]{4})$", ErrorMessage = "Not a valid phone number")]//Valida con expresion Regular
        public String Telefono { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        [EmailAddress(ErrorMessage = "Correo Invalido")]
        public String Correo { get; set; }
        public int Clv { get; set; }
        public int Habilitado { get; set; }

        //public ICollection<ComProcls> ComproRel { get; set; }
        //public ICollection<CompraDbcls> Compra { get; set; }

    }
}
