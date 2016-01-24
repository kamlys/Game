using Game.Core.DTO;
using Game.GUI.ViewModels.Product.UserProduct;
using Game.Service.Interfaces.TableInterface;
using System.Linq;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class UserProductController : Controller
    {
        private IUserProductService _userProductTable;

        public UserProductController(IUserProductService userProductTable)
        {
            _userProductTable = userProductTable;
        }

        [Authorize]
        // GET: UserProduct
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public void CreateProduct(int value, string productname)
        {
            _userProductTable.CreateProduct(value, productname, User.Identity.Name);
        }
    }
}