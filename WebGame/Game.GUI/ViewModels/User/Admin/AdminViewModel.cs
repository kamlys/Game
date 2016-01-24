using PagedList;
using System.Collections.Generic;

namespace Game.GUI.ViewModels.User.Admin
{
    public class AdminViewModel
    {
        public string[] allUser { get; set; }
        public ItemAdminViewModel viewModel { get; set; }
        public List<ItemAdminViewModel> listModel { get; set; }
    }
}