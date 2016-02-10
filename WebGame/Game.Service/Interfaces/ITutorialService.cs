using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface ITutorialService
    {
        TutorialDto tutorialUser(string user);
        void noTutorial(string user);
        void closeTutorial(string user, string div);
    }
}
