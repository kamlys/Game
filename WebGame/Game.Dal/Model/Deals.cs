namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Deals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Deals()
        {
            DealsBuildings = new HashSet<DealsBuildings>();
        }

        public int ID { get; set; }

        public int User1_ID { get; set; }

        public int User2_ID { get; set; }

        public int Building_ID { get; set; }

        public int Map_ID { get; set; }

        public int Percent_User1 { get; set; }

        public int Percent_User2 { get; set; }

        public bool IsActive { get; set; }

        public virtual Buildings Buildings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealsBuildings> DealsBuildings { get; set; }

        public virtual Maps Maps { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
