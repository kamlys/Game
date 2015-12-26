using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building
{
    public class AjaxBuildViewModel
    {
        public int Id { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public bool Owner { get; set; }
        public int Percent_User1 { get; set; }
        public bool Deal { get; set; }
        public int DealID { get; set; }
    }
}