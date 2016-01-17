using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Dolar
{
    public class DolarViewModel
    {
        public ItemDolarViewModel viewModel { get; set; }
        public IPagedList<ItemDolarViewModel> listModel { get; set; }
    }
}