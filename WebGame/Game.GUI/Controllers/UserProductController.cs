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
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _userProductTable.GetUserProduct())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    User_ID = item.User_ID,
                    Login = item.Login,
                    Name = item.Product_Name,
                    Value = item.Value,
                    Product_ID = item.Product_ID
                });
            }


            return View("~/Views/Admin/_UserProductList.cshtml", tableList);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(ListTableViewModel listView)
        {
            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.Login = listView.tableView.Login;
            _userProductDto.Product_Name = listView.tableView.Product_Name;
            _userProductDto.Value =  listView.tableView.Value;

            _userProductTable.Add(_userProductDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(ListTableViewModel listView)
        {
            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.ID = listView.tableView.ID;
            _userProductDto.Login = listView.tableView.Login;
            _userProductDto.Product_Name = listView.tableView.Product_Name;
            _userProductDto.Value = listView.tableView.Value;

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
    }
}