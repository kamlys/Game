namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tutorials
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        public bool cookies { get; set; }

        public bool allDiv { get; set; }

        public bool homeDiv { get; set; }

        public bool officeDiv { get; set; }

        public bool marketDiv { get; set; }

        public bool messageDiv { get; set; }

        public bool casinoDiv { get; set; }

        public bool rankDiv { get; set; }

        public bool setDiv { get; set; }

        public virtual Users Users { get; set; }
    }
}
