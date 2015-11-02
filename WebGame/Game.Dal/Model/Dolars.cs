namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dolars
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        public int Value { get; set; }

        public virtual Users Users { get; set; }
    }
}
