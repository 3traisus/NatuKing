using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Empcom
    {
        public int Id { get; set; }
        public int Clvcom { get; set; }
        public int Idempcom { get; set; }

        public virtual Compra ClvcomNavigation { get; set; } = null!;
        public virtual Empleado IdNavigation { get; set; } = null!;
    }
}
