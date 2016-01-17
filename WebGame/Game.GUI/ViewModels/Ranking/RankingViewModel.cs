using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Ranking
{
    public class RankingViewModel
    {
        public int UserID { get; set; }
        public string User_Login { get; set; }
        public int UserDolar { get; set; }
        public int Position { get; set; }
    }
}