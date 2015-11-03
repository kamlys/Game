namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Buildings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Buildings()
        {
            UserBuildings = new HashSet<UserBuildings>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int Price { get; set; }

        public int Percent_price_per_lvl { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Product_per_sec { get; set; }

        public double Dest_price { get; set; }

        public int Percent_product_per_lvl { get; set; }

        public int Product_ID { get; set; }
        public string Alias { get; set; }

        public virtual Products Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserBuildings> UserBuildings { get; set; }
    }
}
