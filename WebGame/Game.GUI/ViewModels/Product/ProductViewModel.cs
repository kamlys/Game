using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Product
{
    public class ProductViewModel
    {
        public ItemProductViewModel viewModel { get; set; }
        public IPagedList listModel { get; set; }
    }
}