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
        public ActionResult _UserProductList()
        {
            UserProductViewModel userProductModel = new UserProductViewModel();

            userProductModel.listModel = _userProductTable.GetUserProduct().Select(x => new ItemUserProductViewModel
            {
                ID = x.ID,
                User_ID = x.User_ID,
                User_Login = x.Login,
                Product_Name = x.Product_Name,
                Value = x.Value,
                Product_ID = x.Product_ID
            }).ToList();

            return View("~/Views/Admin/_UserProductList.cshtml", userProductModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(UserProductViewModel userProductModel)
        {
            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.Login = userProductModel.viewModel.User_Login;
            _userProductDto.Product_Name = userProductModel.viewModel.Product_Name;
            _userProductDto.Value = userProductModel.viewModel.Value;

            _userProductTable.Add(_userProductDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(UserProductViewModel userProductModel)
        {
            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.ID = userProductModel.viewModel.ID;
            _userProductDto.Login = userProductModel.viewModel.User_Login;
            _userProductDto.Product_Name = userProductModel.viewModel.Product_Name;
            _userProductDto.Value = userProductModel.viewModel.Value;

            _userProductTable.Update(_userProductDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _userProductTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

        [Authorize]
        public void CreateProduct(int value, string productname)
        {
            _userProductTable.CreateProduct(value, productname, User.Identity.Name);
        }
    }
}