﻿using Game.Service.Interfaces.TableInterface;
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

        public bool Add(BanDto ban)
        {
            if (!_bans.GetAll().Any(i => i.Users.Login == ban.Login))
            {
                try
                {
                    _bans.Add(new Bans
                    {
                        User_ID = _users.GetAll().First(i => i.Login == ban.Login).ID,
                        Description = ban.Description,
                        Start_Date = DateTime.Now,
                        Finish_Date = ban.Finish_Date
                    });

                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                _bans.Delete(_bans.Get(id));
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

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
                        Login = _users.GetAll().First(i => i.ID == item.User_ID).Login,
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

        public bool Update(BanDto ban)
        {
            if (_bans.GetAll().Any(i => i.ID == ban.ID))
            {
                try
                {
                    foreach (var item in _bans.GetAll().Where(i => i.ID == ban.ID))
                    {
                        item.User_ID = _users.GetAll().First(i => i.Login == ban.Login).ID;
                        item.Description = ban.Description;
                        item.Finish_Date = ban.Finish_Date;
                    }

                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
