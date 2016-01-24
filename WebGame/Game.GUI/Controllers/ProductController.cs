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
    }
}