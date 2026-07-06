using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPlantao.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
