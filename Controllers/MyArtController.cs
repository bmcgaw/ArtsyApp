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

        public IActionResult Welcome(string Jerry, int ID = 1)
        {
            ViewData["Jerry"] = "Jerry Seinfeld";
            ViewData["ID"] = ID + 1;
            return View();
        }
    }
}
