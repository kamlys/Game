using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Model;
using Game.Dal.Repository;
using Game.Dal;
using Game.Service.Interfaces.TableInterface;

namespace Game.Service.Table
{
    public class AdminTableService : IAdminTableService
    {
        private IRepository<Admins> _admins;
        private IUnitOfWork _unitOfWork;

        public AdminTableService(IRepository<Admins> admins,
            IUnitOfWork unitOfWork)
        {
            _admins = admins;
            _unitOfWork = unitOfWork;
        }

        public void Add(AdminDto admin)
        {
            _admins.Add(new Admins
            {
                User_ID = admin.User_ID
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AdminDto> GetAdmins()
        {
            List<AdminDto> adminDto = new List<AdminDto>();
            foreach (var item in _admins.GetAll())
            {
                try
                {
                    adminDto.Add(new AdminDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID
                    });
                }
                catch (Exception)
                {
                }
            }
            return adminDto;
        }

        public void Update(AdminDto admin, int id)
        {
            throw new NotImplementedException();
        }
    }
}
