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
    public class ProductController : Controller
    {
        private IProductTableService _productTable;


        public ProductController(IProductTableService productTable)
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
            List<TableViewModel> adminViewModel = new List<TableViewModel>();

            foreach (var item in _productTable.GetProduct())
            {
                adminViewModel.Add(new TableViewModel
                {
                    ID = item.ID,
                    Name = item.Name,
                    Price_per_unit = item.Price_per_unit,
                    Unit = item.Unit,
                    Alias = item.Alias
                });
            }


            return View("~/Views/Admin/_ProductList.cshtml", adminViewModel);
        }

        [HttpPost]
        public ActionResult Add(string Name, int Price_per_unit, string Unit, string Alias)
        {
            ProductDto _productDto = new ProductDto();

            _productDto.Name = Name;
            _productDto.Price_per_unit = Price_per_unit;
            _productDto.Unit = Unit;
            _productDto.Alias = Alias;

            _productTable.Add(_productDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}