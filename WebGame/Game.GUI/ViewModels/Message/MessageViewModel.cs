using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Message
{
    public class MessageViewModel
    {
        public string[] userList { get; set; }
        public ItemMessageViewModel viewModel { get; set; }
        public IPagedList<ItemMessageViewModel> pagedList { get; set; }
        public List<ItemMessageViewModel> listModel { get; set; }
    }
}