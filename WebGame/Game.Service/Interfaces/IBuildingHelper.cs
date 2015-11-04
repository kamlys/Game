using Game.Core.DTO;
using Game.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IBuildingHelper
    {
        List<BuildingDto> GetBuildings();
        List<UserBuildingDto> GetBuildings(string User);

    }
}
