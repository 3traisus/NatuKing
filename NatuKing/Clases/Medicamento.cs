using System.ComponentModel.DataAnnotations;
namespace NatuKing.Clases
{
    public class Medicamento
    {
        public int Clv { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression("[0-9]{13}", ErrorMessage = "Codigo no valido:length(13)")]//Valida con expresion Regular
        public String CodBar { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public String Nombre { get; set;}
        public int ?Existencia { get; set;}//No-A
        public float ?Pcompra { get; set; }//No-A
        public float ?Pventa { get; set; }//No-A
        public String ?Caducidad { get; set;}
        public String ?Cura { get; set; }
        public String ?Descripcion { get; set; }
        public String ?Foto { get; set;}

        public String Presentacion { get; set; }
    }
}
