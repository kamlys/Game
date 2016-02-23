using PagedList;
using System.Collections.Generic;

namespace Game.GUI.ViewModels.Product.UserProduct
{
    public class UserProductViewModel
	{

        public string[] allUser { get; set; }
        public string[] allProduct { get; set; }
        public ItemUserProductViewModel viewModel { get; set; }
        public List<ItemUserProductViewModel> listModel { get; set; }
        public IPagedList<ItemUserProductViewModel> pagedList { get; set; }
    }
}