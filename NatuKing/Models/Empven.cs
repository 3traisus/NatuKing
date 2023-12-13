using System;
using System.Collections.Generic;

namespace NatuKing.Models
{
    public partial class Empven
    {
        public int Id { get; set; }
        public int Clvvent { get; set; }
        public int Idempven { get; set; }

        public virtual Venta ClvventNavigation { get; set; } = null!;
        public virtual Empleado IdNavigation { get; set; } = null!;
    }
}
