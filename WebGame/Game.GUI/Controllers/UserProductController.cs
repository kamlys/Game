using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class UserProductController : Controller
    {
        private IUserProductTableService _userProductTable;

        public UserProductController(IUserProductTableService userProductTable)
        {
            _userProductTable = userProductTable;
        }


        // GET: UserProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _UserProductList()
        {
            List<TableViewModel> adminViewModel = new List<TableViewModel>();

            foreach (var item in _userProductTable.GetUserProduct())
            {
                adminViewModel.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    Name = item.Product_Name,
                    Value = item.Value,
                    Product_ID = item.Product_ID
                });
            }


            return View("~/Views/Admin/_UserProductList.cshtml", adminViewModel);
        }

        [HttpPost]
        public ActionResult Add(int User_ID, string Product_Name, int Value, int Product_ID)
        {
            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.User_ID = User_ID;
            _userProductDto.Product_Name = Product_Name;
            _userProductDto.Value = Value;
            _userProductDto.Product_ID = Product_ID;

            _userProductTable.Add(_userProductDto);

            return View("~/Views/Admin/Admin.cshtml");
        }


    }
}