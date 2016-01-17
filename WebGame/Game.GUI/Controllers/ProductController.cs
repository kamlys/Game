using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Product;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using PagedList;
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

        [Authorize]
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult _ProductList(int? Page_No)
        {
            ProductViewModel productModel = new ProductViewModel();

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);

            productModel.listModel = _productTable.GetProduct().Select(x => new ItemProductViewModel
            {
                ID = x.ID,
                ProductName = x.Name,
                Price_per_unit = x.Price_per_unit,
                Unit = x.Unit,
                Alias = x.Alias
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Admin/_ProductList.cshtml", productModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(ProductViewModel productModel)
        {
            ProductDto _productDto = new ProductDto();

            _productDto.Name = productModel.viewModel.ProductName;
            _productDto.Price_per_unit = productModel.viewModel.Price_per_unit;
            _productDto.Unit = productModel.viewModel.Unit;
            _productDto.Alias = productModel.viewModel.Alias;

            _productTable.Add(_productDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(ProductViewModel productModel)
        {
            ProductDto _productDto = new ProductDto();

            _productDto.ID = productModel.viewModel.ID;
            _productDto.Name = productModel.viewModel.ProductName;
            _productDto.Price_per_unit = productModel.viewModel.Price_per_unit;
            _productDto.Unit = productModel.viewModel.Unit;
            _productDto.Alias = productModel.viewModel.Alias;

            _productTable.Add(_productDto);

            return View("~/Views/Admin/Admin.cshtml");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _productTable.Delete(id);
            return View("~/Views/Admin/Admin.cshtml");
        }

    }
}