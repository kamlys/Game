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
    public class DolarTableService : IDolarTableService
    {
        private IRepository<Dolars> _dolars;
        private IUnitOfWork _unitOfWork;

        public DolarTableService(IRepository<Dolars> dolars,
            IUnitOfWork unitOfWork)
        {
            _dolars = dolars;
            _unitOfWork = unitOfWork;
        }

        public void Add(DolarDto dolar)
        {
            _dolars.Add(new Dolars
            {
                User_ID = dolar.User_ID,
                Value = dolar.Value
            });

            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
                        User_ID = item.User_ID
                    });
                }
                catch (Exception)
                {
                }
            }
            return dolarDto;
        }

        public void Update(DolarDto dolar, int id)
        {
            throw new NotImplementedException();
        }
    }
}
