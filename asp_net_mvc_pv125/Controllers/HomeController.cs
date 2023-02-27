using asp_net_mvc_pv125.Models;
using BusinessLogic.Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace asp_net_mvc_pv125.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        private readonly ShopDbContext context;

        public HomeController(IProductsService productsService, ShopDbContext context)
        {
            this.productsService = productsService;
            this.context = context;
        }

        public IActionResult Index(string searchString, string sortOrder)
        {




            ViewBag.YearSortParm = sortOrder == "Year" ? "Year_desc" : "Year";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";

            var cars = from m in context.Products
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(s => s.Name.StartsWith(searchString));
            }

            switch (sortOrder)
            {
                case "Price":
                    cars = cars.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    cars = cars.OrderByDescending(s => s.Price);
                    break;
                case "Year":
                    cars = cars.OrderBy(s => s.Year);
                    break;
                case "Year_desc":
                    cars = cars.OrderByDescending(s => s.Year);
                    break;
            }

            return View(cars);

            // return View(productsService.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}