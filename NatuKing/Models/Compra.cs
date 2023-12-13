using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Compra
    {
        public Compra()
        {
            Commers = new HashSet<Commer>();
            Compros = new HashSet<Compro>();
            Empcoms = new HashSet<Empcom>();
        }

        public string Fechac { get; set; } = null!;
        public double? Totalc { get; set; }
        public int Clvcom { get; set; }

        public virtual ICollection<Commer> Commers { get; set; }
        public virtual ICollection<Compro> Compros { get; set; }
        public virtual ICollection<Empcom> Empcoms { get; set; }
    }
}
