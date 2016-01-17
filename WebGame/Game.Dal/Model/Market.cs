namespace Game.Dal.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Market")]
    public partial class Market
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        public int Product_ID { get; set; }

        public int Number { get; set; }

        public int Price { get; set; }

        public bool TypeOffer { get; set; }

        public virtual Users Users { get; set; }
    }
}
