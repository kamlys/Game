using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Product.ProductRequirement
{
    public class ItemProductRequirementViewModel
    {
        public int Base_ID { get; set; }
        public string BaseName { get; set; }
        public int Require_ID { get; set; }
        public string RequireName { get; set; }
        public string BuildingName { get; set; }
        public List<Dictionary<string, int>> RequireProduct { get; set; }
        public bool ifCan { get; set; }
    }
}