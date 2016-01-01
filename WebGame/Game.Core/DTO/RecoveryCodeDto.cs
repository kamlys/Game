using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class RecoveryCodeDto
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string Code { get; set; }
        public DateTime LifeTime { get; set; }
    }
}
