using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Model;
using Game.Dal;
using Game.Dal.Repository;

namespace Game.Service.Table
{
    public class BanService : IBanService
    {
        private IRepository<Bans> _bans;
        private IRepository<Users> _users;
        private IUnitOfWork _unitOfWork;

        public BanService(IRepository<Bans> bans,
            IRepository<Users> users,
            IUnitOfWork unitOfWork)
        {
            _bans = bans;
            _users = users;
            _unitOfWork = unitOfWork;
        }

        public void Add(BanDto ban)
        {
            _bans.Add(new Bans
            {
                User_ID = _users.GetAll().First(i=> i.ID == ban.User_ID).ID,
                Description = ban.Description,
                Start_Date = ban.Start_Date,
                Finish_Date = ban.Finish_Date
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _bans.Delete(_bans.Get(id));
            _unitOfWork.Commit();
        }

        public List<BanDto> GetBan()
        {
            List<BanDto> banDto = new List<BanDto>();
            foreach (var item in _bans.GetAll())
            {
                try
                {
                    banDto.Add(new BanDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _users.GetAll().First(i=> i.ID == item.User_ID).Login,
                        Description = item.Description,
                        Start_Date = item.Start_Date,
                        Finish_Date = item.Finish_Date
                    });
                }
                catch (Exception)
                {
                }
            }
            return banDto;
        }

        public void Update(BanDto admin, int id)
        {
            throw new NotImplementedException();
        }
    }
}
