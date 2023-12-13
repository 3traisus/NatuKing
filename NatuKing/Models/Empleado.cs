using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Empcoms = new HashSet<Empcom>();
            Empvens = new HashSet<Empven>();
        }

        public string Nombree { get; set; } = null!;
        public string Puesto { get; set; } = null!;
        public string Horario { get; set; } = null!;
        public double Sueldo { get; set; }
        public string Telefonoe { get; set; } = null!;
        public string Correoe { get; set; } = null!;
        public string Ppass { get; set; } = null!;
        public int Habilitado { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Empcom> Empcoms { get; set; }
        public virtual ICollection<Empven> Empvens { get; set; }
    }
}
