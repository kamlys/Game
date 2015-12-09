namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        public int ID { get; set; }

        public int Sender_ID { get; set; }

        public int Customer_ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Theme { get; set; }

        [Required]
        public string Content { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PostDate { get; set; }

        public bool IfRead { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
