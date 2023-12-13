using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Venta
    {
        public Venta()
        {
            Empvens = new HashSet<Empven>();
            Venmers = new HashSet<Venmer>();
        }

        public string Fechav { get; set; } = null!;
        public double? Totalv { get; set; }
        public int Clvvent { get; set; }

        public virtual ICollection<Empven> Empvens { get; set; }
        public virtual ICollection<Venmer> Venmers { get; set; }
    }
}
