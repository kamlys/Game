namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bans
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime Finish_Date { get; set; }

        public virtual Users Users { get; set; }
    }
}
