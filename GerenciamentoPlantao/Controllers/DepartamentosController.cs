using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Exceptions;

namespace GerenciamentoPlantao.Controllers
{
    [Authorize]
    public class DepartamentosController : Controller
    {

        public readonly DepartamentoService _departamentoService;

        public DepartamentosController(DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index(string? ordenarPor, string? direcao, string? busca, int paginaAtual = 1)
        {
            var tamanhoPagina = 20;

            var lista = await _departamentoService.FindAllAsync(paginaAtual, tamanhoPagina, busca, ordenarPor, direcao);
            
            ViewBag.Busca = busca;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.Direcao = direcao;

            return View(lista); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartamentoFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _departamentoService.InserirDepartamentoAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            var departamento = await _departamentoService.FindByIdWithAuditAsync(id.Value);
            var vm = new EditarDepartamentoViewModel
            {
                Id = departamento.Id,
                Nome = departamento.Nome,
                Ativo = departamento.Ativo,
                UsuarioInsertNome = departamento.UsuarioInsert?.DescNome,
                UsuarioUpdateNome = departamento.UsuarioUpdate?.DescNome,
                UsuarioInativacaoNome = departamento.UsuarioInativacao?.DescNome,
                DataInsert = departamento.DataInsert,
                DataUpdate = departamento.DataUpdate,
                DataInativacao = departamento.DataInativacao
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarDepartamentoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _departamentoService.AlterarDepartamentoAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            var departamento = await _departamentoService.FindByIdAsync(id.Value);
            return View(departamento);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(int id)
        {
            await _departamentoService.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            var departamento = await _departamentoService.FindByIdAsync(id.Value);
            return View(departamento);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(int id)
        {
            await _departamentoService.AtivarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
