using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace ArtsyApp.Controllers
{
    public class MyArtController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, int numHello = 1)
        {
            ViewData["NumTimes"] = numHello;
            ViewData["Message"] = $"Hello, {name}";
            return View();
        }
    }
}
