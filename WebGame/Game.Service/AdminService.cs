using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Repository;
using Game.Dal.Model;
using Game.Dal;

namespace Game.Service
{
    public class AdminService : IAdminService
    {
        private IRepository<Users> _users;
        private IRepository<Admins> _admins;
        private IRepository<Bans> _bans;
        private IRepository<BuildingQueue> _buildingQueue;
        private IRepository<Buildings> _buildings;
        private IRepository<Dolars> _dolars;
        private IRepository<Maps> _maps;
        private IRepository<Products> _products;
        private IRepository<UserBuildings> _userBuildings;
        private IRepository<UserProducts> _userProducts;

        private IUnitOfWork _unitOfWork;
        private IHashPass _hassPass;

        public AdminService(
            IRepository<Users> users,
            IRepository<Admins> admins,
            IRepository<Bans> bans,
            IRepository<BuildingQueue> buildingQueue,
            IRepository<Buildings> buildings,
            IRepository<Dolars> dolars,
            IRepository<Maps> maps,
            IRepository<Products> products,
            IRepository<UserBuildings> userBuildings,
            IRepository<UserProducts> userProducts,
        IHashPass hassPass,
            IUnitOfWork unitOfWork)
        {
            _users = users;
            _admins = admins;
            _bans = bans;
            _buildingQueue = buildingQueue;
            _buildings = buildings;
            _dolars = dolars;
            _maps = maps;
            _products = products;
            _userBuildings = userBuildings;
            _userProducts = userProducts;
            _hassPass = hassPass;
            _unitOfWork = unitOfWork;
        }


        public bool ifAdmin(string User)
        {
            int uID = _users.GetAll().First(u => u.Login == User).ID;

                if (_admins.GetAll().Any(i=> i.User_ID == uID))
                {
                    return true;
                }
            return false;
        }

        public List<AdminDto> GetAdmin()
        {
            List<AdminDto> adminDto = new List<AdminDto>();
            foreach (var item in _admins.GetAll())
            {
                try
                {
                    adminDto.Add(new AdminDto
                    {
                        ID = item.ID,
                        Login = _users.Get(item.User_ID).Login
                    });
                }
                catch (Exception)
                {
                }
            }
            return adminDto;
        }
        public void Add(AdminDto admin)
        {
            _admins.Add(new Admins
            {
                User_ID = _users.GetAll().First(i => i.Login == admin.Login).ID
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _admins.Delete(_admins.Get(id));
            _unitOfWork.Commit();
        }

        public void Update(AdminDto admin)
        {
            foreach (var item in _admins.GetAll().Where(i => i.ID == admin.ID))
            {
                item.User_ID = _users.GetAll().First(i=> i.Login == admin.Login).ID;
            }

            _unitOfWork.Commit();
        }
    }
}
