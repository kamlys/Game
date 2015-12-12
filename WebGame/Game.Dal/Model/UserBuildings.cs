namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserBuildings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserBuildings()
        {
            BuildingQueue = new HashSet<BuildingQueue>();
        }

        public int ID { get; set; }

        public int User_ID { get; set; }

        public int X_pos { get; set; }

        public int Y_pos { get; set; }

        public int Lvl { get; set; }

        public int Building_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public bool Owner { get; set; }

        public int Percent_product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BuildingQueue> BuildingQueue { get; set; }

        public virtual Buildings Buildings { get; set; }

        public virtual Users Users { get; set; }
    }
}
