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
using System.Data;
using System.Reflection;

namespace Game.Service
{
    public class TutorialService : ITutorialService
    {
        private IRepository<Users> _users;
        private IRepository<Tutorials> _tutorials;
        private IUnitOfWork _unitOfWork;

        public TutorialService(IRepository<Users> users, IRepository<Tutorials> tutorials, IUnitOfWork unitOfWork)
        {
            _users = users;
            _tutorials = tutorials;
            _unitOfWork = unitOfWork;
        }

        public void closeTutorial(string user, string div)
        {
            int uID = _users.GetAll().First(i => i.Login == user).ID;

            Type tutorialDiv = typeof(Tutorials);


            foreach (var item in _tutorials.GetAll().Where(i => i.User_ID == uID))
            {
                foreach (PropertyInfo propertyInfo in tutorialDiv.GetProperties())
                {
                    if (propertyInfo.Name == div)
                    {
                        propertyInfo.SetValue(item, true);
                    }
                }
            }

            _unitOfWork.Commit();
        }
        public void noTutorial(string user)
        {
            int uID = _users.GetAll().First(i => i.Login == user).ID;

            foreach (var item in _tutorials.GetAll().Where(i => i.User_ID == uID))
            {
                item.allDiv = true;
                item.casinoDiv = true;
                item.marketDiv = true;
                item.messageDiv = true;
                item.rankDiv = true;
                item.setDiv = true;
                item.homeDiv = true;
                item.officeDiv = true;
            }
            _unitOfWork.Commit();
        }

        public TutorialDto tutorialUser(string user)
        {
            int uID = _users.GetAll().First(i => i.Login == user).ID;

            TutorialDto tutorialDto = new TutorialDto();

            foreach (var item in _tutorials.GetAll().Where(i => i.User_ID == uID))
            {
                tutorialDto.casinoDiv = item.casinoDiv;
                tutorialDto.marketDiv = item.marketDiv;
                tutorialDto.messageDiv = item.messageDiv;
                tutorialDto.officeDiv = item.officeDiv;
                tutorialDto.rankDiv = item.rankDiv;
                tutorialDto.setDiv = item.setDiv;
                tutorialDto.allDiv = item.allDiv;
                tutorialDto.cookies = item.cookies;
                tutorialDto.homeDiv = item.homeDiv;
            }

            return tutorialDto;

        }
    }
}
