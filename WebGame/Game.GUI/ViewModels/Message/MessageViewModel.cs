using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Message
{
    public class MessageViewModel
    {
        public ItemMessageViewModel viewModel { get; set; }
        public IPagedList<ItemMessageViewModel> listModel { get; set; }
    }
}