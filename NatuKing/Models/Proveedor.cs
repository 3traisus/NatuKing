using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Compros = new HashSet<Compro>();
        }

        public string Nombrep { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string Cp { get; set; } = null!;
        public string Telefonop { get; set; } = null!;
        public string? Correop { get; set; }
        public int Habilitado { get; set; }
        public int Clvpro { get; set; }

        public virtual ICollection<Compro> Compros { get; set; }
    }
}
