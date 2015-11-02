namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Admins = new HashSet<Admins>();
            Bans = new HashSet<Bans>();
            Maps = new HashSet<Maps>();
            UserBuildings = new HashSet<UserBuildings>();
            UserProducts = new HashSet<UserProducts>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Last_Log { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Registration_Date { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Last_Update { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admins> Admins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bans> Bans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Maps> Maps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserBuildings> UserBuildings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProducts> UserProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dolars> Dolars { get; set; }
    }
}
