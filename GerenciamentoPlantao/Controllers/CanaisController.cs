using GerenciamentoPlantao.Models.ViewModels;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Controllers
{
    public class CanaisController : Controller
    {   
        private readonly CanalService _canalService;
        private readonly DepartamentoService _departamentoService;

        public CanaisController(CanalService canalService, DepartamentoService departamentoService)
        {
            _canalService = canalService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _canalService.FindAllAsync();
            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new CanalFormViewModel
            {
                Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                })
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CanalFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                });
            }

            await _canalService.CriarCanalAsync(vm.DepartamentoId, vm.Nome);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var canal = await _canalService.FindByIdAsync(id.Value);
            var vm = new EditarCanalViewModel
            {
                Id = canal.Id,
                Nome = canal.Nome,
                Ativo = canal.Ativo,
                DepartamentoId = canal.DepartamentoId,

                Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                })
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarCanalViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                });
            }

            await _canalService.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canal = await _canalService.FindByIdAsync(id.Value);
            if (canal == null)
            {
                return NotFound();
            }

            return View(canal);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(int id)
        {
            await _canalService.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canal = await _canalService.FindByIdAsync(id.Value);

            if (canal == null)
            {
                return NotFound();
            }

            return View(canal);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(int id)
        {
            await _canalService.AtivarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
