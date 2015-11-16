using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels
{
    public class ListTableViewModel : TableViewModel
    {
        public TableViewModel tableView { get; set; }
        public List<TableViewModel> tableList { get; set; }
    }
}