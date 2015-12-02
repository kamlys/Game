using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productTable;


        public ProductController(IProductService productTable)
        {
            _productTable = productTable;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _ProductList()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _productTable.GetProduct())
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Name = item.Name,
                    Price_per_unit = item.Price_per_unit,
                    Unit = item.Unit,
                    Alias = item.Alias
                });
            }


            return View("~/Views/Admin/_ProductList.cshtml", tableList);
        }

        [HttpPost]
        public ActionResult Add(ListTableViewModel listView)
        {
            ProductDto _productDto = new ProductDto();

            _productDto.Name = listView.tableView.Name;
            _productDto.Price_per_unit = listView.tableView.Price_per_unit;
            _productDto.Unit = listView.tableView.Unit;
            _productDto.Alias = listView.tableView.Alias;

            _productTable.Add(_productDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        public ActionResult Update(ListTableViewModel listView)
        {
            ProductDto _productDto = new ProductDto();

            _productDto.ID = listView.tableView.ID;
            _productDto.Name = listView.tableView.Name;
            _productDto.Price_per_unit = listView.tableView.Price_per_unit;
            _productDto.Unit = listView.tableView.Unit;
            _productDto.Alias = listView.tableView.Alias;

            _productTable.Add(_productDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _productTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}