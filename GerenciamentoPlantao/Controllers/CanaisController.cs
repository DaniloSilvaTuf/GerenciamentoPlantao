using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPlantao.Controllers
{
    public class CanaisController : Controller
    {
        
        private readonly CanalService _canalService;
        private readonly DepartamentoService _departamentoService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
