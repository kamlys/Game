﻿using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IDealService
    {
        List<DealDto> GetUserDeals(string User);
        int[] AddDeal(DealDto dealDto);
        bool AcceptDeal(int ID, string user);
        void CancelDeal(int ID);
        void RerunDeal(int ID, string user);

    }
}