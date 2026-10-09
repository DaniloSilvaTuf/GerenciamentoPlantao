using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Services;
using GerenciamentoPlantao.Services.Escalas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Areas.Escalas.Controllers
{
    [Area("Escalas")]
    [Authorize]
    public class PostoEscalaController : Controller
    {

        private readonly PostoEscalaService _postoEscalaService;
        private readonly DepartamentoService _departamentoService;

        public PostoEscalaController(PostoEscalaService postoEscalaService, DepartamentoService departamentoService)
        {
            _postoEscalaService = postoEscalaService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index(string? ordenarPor, string? direcao, string? busca, int? departamentoId, int paginaAtual = 1)
        {
            var tamanhoPagina = 20;

            var lista = await _postoEscalaService.FindAllAsync(paginaAtual, tamanhoPagina, busca, ordenarPor, direcao, departamentoId);

            ViewBag.Busca = busca;
            ViewBag.Direcao = direcao;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.DepartamentoId = departamentoId;

            ViewBag.Departamentos = await _departamentoService.FindAllActiveAsync();

            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new PostoEscalaFormViewModel
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
        public async Task<IActionResult> Create(PostoEscalaFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                });
                return View(vm);
            }

            await _postoEscalaService.CriarPostoAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id, int? departamentoId)
        {
            if (!id.HasValue || !departamentoId.HasValue)
            {
                throw new NotFoundException("Posto não encontrado");
            }

            var posto = await _postoEscalaService.FindByIdWithAuditAsync(id.Value, departamentoId.Value);
            var vm = new EditarPostoEscalaViewModel
            {
                Id = posto.Id,
                Nome = posto.Nome,
                DepartamentoId = posto.DepartamentoId,
                DepartamentoNome = posto.Departamento.Nome,
                Ativo = posto.Ativo,
                DataInsert = posto.DataInsert,
                UsuarioInsertNome = posto.UsuarioInsert?.DescNome,
                DataUpdate = posto.DataUpdate,
                UsuarioUpdateNome = posto.UsuarioUpdate?.DescNome,
                DataInativacao = posto.DataInativacao,
                UsuarioInativacaoNome = posto.UsuarioInativacao?.DescNome
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarPostoEscalaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _postoEscalaService.AtualizarPostoAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id, int? departamentoId)
        {
            if (!id.HasValue || !departamentoId.HasValue)
            {
                throw new NotFoundException("Posto não encontrado");
            }

            var posto = await _postoEscalaService.FindByIdAsync(id.Value, departamentoId.Value);
            return View(posto);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(int id, int departamentoId)
        {
            await _postoEscalaService.InativarPostoAsync(id, departamentoId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(int? id, int? departamentoId)
        {
            if (!id.HasValue || !departamentoId.HasValue)
            {
                throw new NotFoundException("Posto não encontrado");
            }
            var posto = await _postoEscalaService.FindByIdAsync(id.Value, departamentoId.Value);
            return View(posto);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(int id, int departamentoId)
        {
            await _postoEscalaService.AtivarPostoAsync(id, departamentoId);
            return RedirectToAction(nameof(Index));
        }
    }
}
