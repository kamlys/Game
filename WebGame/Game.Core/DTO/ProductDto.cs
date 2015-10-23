using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class ProductDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price_per_unit { get; set; }
        public string Unit { get; set; }
    }
}
