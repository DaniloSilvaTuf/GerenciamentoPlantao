using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;

namespace GerenciamentoPlantao.Controllers
{
    [Authorize]
    public class SetoresController : Controller
    {

        private readonly SetorService _setorService;
        private readonly EstabelecimentoService _estabelecimentoService;

        public SetoresController(SetorService setorService, EstabelecimentoService estabelecimentoService)
        {
            _setorService = setorService;
            _estabelecimentoService = estabelecimentoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _setorService.FindAllAsync();
            return View(lista);
        }

        public async Task <IActionResult> Create()
        {
            var vm = new SetorFormViewModel
            {
                Estabelecimentos = (await _estabelecimentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                })
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SetorFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Estabelecimentos = (await _estabelecimentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                });
            }

            await _setorService.InserirSetorAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setor = await _setorService.FindByIdWithAuditAsync(id.Value);
            var vm = new EditarSetorViewModel
            {
                Id = setor.Id,
                Nome = setor.Nome,
                Ativo = setor.Ativo,
                EstabelecimentoId = setor.EstabelecimentoId,

                Estabelecimentos = (await _estabelecimentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value= e.Id.ToString(),
                    Text = e.Nome
                }),

                DataInsert = setor.DataInsert,
                UsuarioInsertNome = setor.UsuarioInsert?.DescNome,
                DataUpdate = setor.DataUpdate,
                UsuarioUpdateNome = setor.UsuarioUpdate?.DescNome,
                DataInativacao = setor.DataInativacao,
                UsuarioInativacaoNome = setor.UsuarioInativacao?.DescNome
            }; 

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarSetorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Estabelecimentos = (await _estabelecimentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                });
            }

            await _setorService.AlterarSetorAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setor = await _setorService.FindByIdAsync(id.Value);
            return View(setor);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(int id)
        {
            await _setorService.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setor = await _setorService.FindByIdAsync(id.Value);
            return View(setor);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(int id)
        {
            await _setorService.AtivarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
