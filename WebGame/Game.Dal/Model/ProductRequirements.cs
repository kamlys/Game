namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductRequirements
    {
        public int ID { get; set; }

        public int Base_ID { get; set; }

        public int Require_ID { get; set; }

        public int Value { get; set; }

        public virtual Products Products { get; set; }

        public virtual Products Products1 { get; set; }
    }
}
