namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notifications
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        public int Temp_ID { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        public bool IfRead { get; set; }

        public virtual Users Users { get; set; }
    }
}
