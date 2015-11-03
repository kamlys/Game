namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserBuildings
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        public int X_pos { get; set; }

        public int Y_pos { get; set; }

        public int Lvl { get; set; }

        public int Building_ID { get; set; }

        public virtual Buildings Buildings { get; set; }

        public virtual Users Users { get; set; }
    }
}
