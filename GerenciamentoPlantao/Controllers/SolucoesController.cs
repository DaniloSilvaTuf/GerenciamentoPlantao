using GerenciamentoPlantao.Models.ViewModels;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Controllers
{
    public class SolucoesController : Controller
    {

        private readonly SolucaoService _solucaoService;
        private readonly DepartamentoService _departamentoService;

        public SolucoesController(SolucaoService solucaoService, DepartamentoService departamentoService)
        {
            _solucaoService = solucaoService;
            _departamentoService = departamentoService;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _solucaoService.FindAllAsync();
            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new SolucaoFormViewModel
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
        public async Task<IActionResult> Create(SolucaoFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                });
            }

            await _solucaoService.CriarSolucaoAsync(vm.DepartamentoId, vm.Nome);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var solucao = await _solucaoService.FindByIdAsync(id.Value);
            var vm = new EditarSolucaoViewModel
            {
                Id = solucao.Id,
                Nome = solucao.Nome,
                Ativo = solucao.Ativo,
                DepartamentoId = solucao.DepartamentoId,

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
        public async Task<IActionResult> Edit(EditarSolucaoViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                });
            }

            await _solucaoService.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solucao = await _solucaoService.FindByIdAsync(id.Value);
            if (solucao == null)
            {
                return NotFound();
            }

            return View(solucao);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(int id)
        {
            await _solucaoService.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solucao = await _solucaoService.FindByIdAsync(id.Value);

            if (solucao == null)
            {
                return NotFound();
            }

            return View(solucao);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(int id)
        {
            await _solucaoService.AtivarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
