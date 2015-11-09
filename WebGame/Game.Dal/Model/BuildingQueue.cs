namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BuildingQueue")]
    public partial class BuildingQueue
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        public int UserBuilding_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? FinishTime { get; set; }

        [Required]
        [StringLength(50)]
        public string NewStatus { get; set; }

        public virtual UserBuildings UserBuildings { get; set; }

        public virtual Users Users { get; set; }
    }
}
