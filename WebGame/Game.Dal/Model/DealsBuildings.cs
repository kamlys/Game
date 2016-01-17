namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DealsBuildings
    {
        public int ID { get; set; }

        public int Building_ID { get; set; }

        public int User_ID { get; set; }

        public int Deal_ID { get; set; }

        public virtual Buildings Buildings { get; set; }

        public virtual Deals Deals { get; set; }

        public virtual Users Users { get; set; }
    }
}
