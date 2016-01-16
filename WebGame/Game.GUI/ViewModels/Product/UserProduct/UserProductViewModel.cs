using PagedList;
using System.Collections.Generic;

namespace Game.GUI.ViewModels.Product.UserProduct
{
    public class UserProductViewModel
	{
        public ItemUserProductViewModel viewModel { get; set; }
        public List<ItemUserProductViewModel> listModel { get; set; }
        public IPagedList<ItemUserProductViewModel> pagedList { get; set; }
    }
}