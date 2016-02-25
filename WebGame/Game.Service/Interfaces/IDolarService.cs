using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces.TableInterface
{
    public interface IDolarService
    {
        List<DolarDto> GetDolars();
        List<DolarDto> GetToRank();
        bool Add(DolarDto admin);
        bool Update(DolarDto admin);
        //bool Delete(int id);
        int UserDolar(string user);
        void PayForBet(string user, int bet);
        int AddFromBet(string user, int bet, int mul);


    }
}
