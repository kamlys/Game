namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Maps
    {
        [Key]
        public int Map_ID { get; set; }

        public int User_ID { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public virtual Users Users { get; set; }
    }
}
