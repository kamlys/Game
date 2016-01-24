using PagedList;
using System.Collections.Generic;

namespace Game.GUI.ViewModels.User.Ban
{
    public class BanViewModel
    {
        public string[] allUser { get; set; }
        public ItemBanViewModel viewModel { get; set; }
        public List<ItemBanViewModel> listModel { get; set; }
    }
}