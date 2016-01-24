using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Dolar
{
    public class DolarViewModel
    {

        public string[] allUser { get; set; }
        public ItemDolarViewModel viewModel { get; set; }
        public List<ItemDolarViewModel> listModel { get; set; }
    }
}