using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.User.Ignored
{
    public class IgnoredViewModel
    {
        public string[] allUser { get; set; }

        public ItemIgnoredViewModel viewModel { get; set; }
        public List<ItemIgnoredViewModel> listModel { get; set; }
    }
}