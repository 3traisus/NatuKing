using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Compro
    {
        public int Clvcom { get; set; }
        public int Clvpro { get; set; }
        public int Idcompro { get; set; }

        public virtual Compra ClvcomNavigation { get; set; } = null!;
        public virtual Proveedor ClvproNavigation { get; set; } = null!;
    }
}
