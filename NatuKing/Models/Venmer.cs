using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Venmer
    {
        public int? Cantidadvm { get; set; }
        public int Clvmer { get; set; }
        public int Clvvent { get; set; }
        public int Idvenmer { get; set; }

        public virtual Mercancium ClvmerNavigation { get; set; } = null!;
        public virtual Venta ClvventNavigation { get; set; } = null!;
    }
}
