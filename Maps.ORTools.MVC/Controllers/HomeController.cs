using Maps.ORTools.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Maps.ORTools.BAL.VRP;
using Maps.ORTools.BAL.Data;

namespace Maps.ORTools.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            VRPSolution v = new VRPSolution();



            ViewBag.CoordinateMatrixSolution = v.GetSolution(Coordinates.CoordinateMatrix);

            


            return View();
        }

        public IActionResult Privacy()
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