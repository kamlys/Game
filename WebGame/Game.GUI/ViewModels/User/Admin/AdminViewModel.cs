using PagedList;

namespace Game.GUI.ViewModels.User.Admin
{
    public class AdminViewModel
    {
        public ItemAdminViewModel viewModel { get; set; }
        public IPagedList<ItemAdminViewModel> listModel { get; set; }
    }
}