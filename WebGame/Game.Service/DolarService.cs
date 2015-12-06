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
    public class DolarService : IDolarService
    {
        private IRepository<Dolars> _dolars;
        private IRepository<Users> _users;
        private IUnitOfWork _unitOfWork;

        public DolarService(IRepository<Dolars> dolars,
            IRepository<Users> users,
            IUnitOfWork unitOfWork)
        {
            _dolars = dolars;
            _users = users;
            _unitOfWork = unitOfWork;
        }

        public void Add(DolarDto dolar)
        {
            _dolars.Add(new Dolars
            {
                User_ID = _users.GetAll().First(i=> i.ID == dolar.User_ID).ID,
                Value = dolar.Value
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _dolars.Delete(_dolars.Get(id));
            _unitOfWork.Commit();
        }

        public List<DolarDto> GetDolars()
        {
            List<DolarDto> dolarDto = new List<DolarDto>();
            foreach (var item in _dolars.GetAll())
            {
                try
                {
                    dolarDto.Add(new DolarDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _users.Get(item.User_ID).Login,
                        Value = item.Value
                    });
                }
                catch (Exception)
                {
                }
            }
            return dolarDto;
        }

        public void Update(DolarDto dolar)
        {
            foreach (var item in _dolars.GetAll().Where(i => i.ID == dolar.ID))
            {
                item.User_ID = _users.GetAll().First(i => i.Login == dolar.Login).ID;
                item.Value = dolar.Value;
            }

            _unitOfWork.Commit();
        }

        public List<DolarDto> GetToRank()
        {
            List<DolarDto> dolarDto = new List<DolarDto>();
            foreach (var item in _dolars.GetAll())
            {
                try
                {
                    dolarDto.Add(new DolarDto
                    {
                        ID = item.ID,
                        User_ID = item.User_ID,
                        Login = _users.Get(item.User_ID).Login,
                        Value = item.Value
                    });

                }
                catch (Exception)
                {
                }
            }
            return dolarDto.OrderByDescending(x=>x.Value).ToList();
        }
    }
}
