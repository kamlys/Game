namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Deals
    {
        public int ID { get; set; }

        public int User1_ID { get; set; }

        public int User2_ID { get; set; }

        public int Building_ID { get; set; }

        public int Map_ID { get; set; }

        public int Percent_User1 { get; set; }

        public int Percent_User2 { get; set; }

        public bool IsActive { get; set; }

        public virtual Buildings Buildings { get; set; }

        public virtual Maps Maps { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
