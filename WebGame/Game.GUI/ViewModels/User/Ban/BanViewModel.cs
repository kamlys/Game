using PagedList;

namespace Game.GUI.ViewModels.User.Ban
{
    public class BanViewModel
    {
        public ItemBanViewModel viewModel { get; set; }
        public IPagedList<ItemBanViewModel> listModel { get; set; }
    }
}