using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Market
{
    public class ListMarketViewModel
    {
        public MarketViewModel marketView { get; set; }
        public List<MarketViewModel> marketList { get; set; }
        public string[] options { get; set; }
    }
}