using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Product.ProductRequirement
{
    public class ProductRequirementViewModel
    {
        public string[] allProduct { get; set; }
        public ItemProductRequirementViewModel viewModel { get; set; }
        public List<ItemProductRequirementViewModel> listModel { get; set; }
        public IPagedList<ItemProductRequirementViewModel> pagedModel { get; set; }
    }
}