using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Mercancium
    {
        public Mercancium()
        {
            Commers = new HashSet<Commer>();
            Venmers = new HashSet<Venmer>();
        }

        public string? Foto { get; set; }
        public string Nombrem { get; set; } = null!;
        public int? Existencia { get; set; }
        public double? Precomp { get; set; }
        public double? Prevent { get; set; }
        public string? Caducidad { get; set; }
        public string? Cura { get; set; }
        public string? Descripcion { get; set; }
        public string Codbar { get; set; } = null!;
        public int Habilitado { get; set; }
        public string? Presentacion { get; set; }
        public int Clvmer { get; set; }

        public virtual ICollection<Commer> Commers { get; set; }
        public virtual ICollection<Venmer> Venmers { get; set; }
    }
}
