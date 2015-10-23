using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class BuildingDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Percent_price_per_lvl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Product_per_sec { get; set; }
        public double Dest_price { get; set; }
        public int Percent_product_per_lvl { get; set; }
        public int Product_ID { get; set; }
    }
}
