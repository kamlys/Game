using PagedList;
using System.Collections.Generic;

namespace Game.GUI.ViewModels.User.Friend
{
    public class FriendViewModel
    {
        public ItemFriendViewModel viewModel { get; set; }
        public List<ItemFriendViewModel> listModel { get; set; }
        public IPagedList<ItemFriendViewModel> pagedList { get; set; }
    }
}