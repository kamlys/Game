using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Market
{
    public class MarketViewModel
    {
        public string[] allProduct { get; set; }
        public string[] userProduct { get; set; }
        public string[] allUser { get; set; }
        public ItemMarketViewModel viewModel { get; set; }
        public List<ItemMarketViewModel> systemOfferList { get; set; }
        public IPagedList<ItemMarketViewModel> pagedList { get; set; }
        public List<ItemMarketViewModel> listModel { get; set; }
    } 
}