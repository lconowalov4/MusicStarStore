using Microsoft.AspNetCore.Mvc;
using MusicStarStore.Models;
using MusicStarStore.Repositories.Abstract;
using MusicStarStore.Repositories.Implementation;
using System.Diagnostics;

namespace MusicStarStore.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IVinylService _vinylService;
        public HomeController(IVinylService vinylService)
        {
            _vinylService = vinylService;
        }

        public IActionResult Index(string term="", int currentPage = 1)
        {
            var vinyls = _vinylService.List(term, true, currentPage);
            return View(vinyls);
        }

        public IActionResult About()
        {
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

        public IActionResult VinylDetail(int vinylId)
        {
            var vinyl = _vinylService.GetById(vinylId);
            return View(vinyl);
        }
    }
}