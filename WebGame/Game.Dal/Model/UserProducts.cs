namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserProducts
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Product_Name { get; set; }

        public int Value { get; set; }

        public int Product_ID { get; set; }

        public string Alias { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }
    }
}
