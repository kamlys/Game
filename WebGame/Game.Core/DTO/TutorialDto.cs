using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class TutorialDto
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public bool homeDiv { get; set; }
        public bool officeDiv { get; set; }
        public bool marketDiv { get; set; }
        public bool messageDiv { get; set; }
        public bool casinoDiv { get; set; }
        public bool rankDiv { get; set; }
        public bool setDiv { get; set; }
        public bool cookies { get; set; }
        public bool allDiv { get; set; }
    }
}
