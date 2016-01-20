using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class ProductRequirementDto
    {
        public int ID { get; set; }
        public int Base_ID { get; set; }
        public string Base_Name { get; set; }
        public int Require_ID { get; set; }
        public string Require_Name { get; set; }
        public int Value { get; set; }
        public string RequireBuilding { get; set; }
        public List<Dictionary<string, int[]>> RequireProduct { get; set; }
        public bool IfCanProduct { get; set; }
    }
}
