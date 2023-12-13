using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Commer
    {
        public int? Cantidadcm { get; set; }
        public int Clvcom { get; set; }
        public int Clvmer { get; set; }
        public int Idcommer { get; set; }

        public virtual Compra ClvcomNavigation { get; set; } = null!;
        public virtual Mercancium ClvmerNavigation { get; set; } = null!;
    }
}
