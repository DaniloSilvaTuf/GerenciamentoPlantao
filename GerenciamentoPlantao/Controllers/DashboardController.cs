using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPlantao.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _dashboardService.ObterDashboardAsync();

            return View(vm);
        }
    }
}
