using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class UserProductDto
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string Login { get; set; }
        public string Product_Name { get; set; }
        public int Value { get; set; }
        public int Product_ID { get; set; }
        public string Alias { get; set; }

    }
}
