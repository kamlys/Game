using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class MapDto
    {
        public int Map_ID { get; set; }
        public int User_ID { get; set; }
        public string Login { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
