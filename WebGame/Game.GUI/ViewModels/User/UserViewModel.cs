using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.User
{
    public class UserViewModel
    {
        public ItemUserViewModel viewModel { get; set; }
        public List<ItemUserViewModel> listModel { get; set; }
        public IPagedList<ItemUserViewModel> pagedList { get; set; }
    }
}