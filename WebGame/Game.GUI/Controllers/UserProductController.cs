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


        // GET: UserProduct
        public ActionResult Index()
        {
            return View();
        }

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
        public ActionResult Add(ListTableViewModel listView)
        {
            UserProductDto _userProductDto = new UserProductDto();

            _userProductDto.User_ID = listView.tableView.User_ID;
            _userProductDto.Login = listView.tableView.Login;
            _userProductDto.Product_Name = listView.tableView.Name;
            _userProductDto.Value =  listView.tableView.Value;
            _userProductDto.Product_ID = listView.tableView.Product_ID;

            _userProductTable.Add(_userProductDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _userProductTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }


    }
}