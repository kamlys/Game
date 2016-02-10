namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RecoveryCodes
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LiveTime { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        public virtual Users Users { get; set; }
    }
}
