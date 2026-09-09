using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Exceptions;

namespace GerenciamentoPlantao.Controllers
{
    [Authorize]
    public class CategoriasAcionamentoController : Controller
    {
        private readonly CategoriaService _categoriaService;
        private readonly DepartamentoService _departamentoService;

        public CategoriasAcionamentoController(CategoriaService categoriaService, DepartamentoService departamentoService)
        {
            _categoriaService = categoriaService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index(string? ordenarPor, string? direcao, string? busca, int? departamentoId, int paginaAtual = 1)
        {
            var tamanhoPagina = 20;

            var lista = await _categoriaService.FindAllAsync(paginaAtual, tamanhoPagina, busca, ordenarPor, direcao, departamentoId);

            ViewBag.Busca = busca;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.Direcao = direcao;
            ViewBag.DepartamentoId = departamentoId;

            ViewBag.Departamentos = await _departamentoService.FindAllActiveAsync();

            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new CategoriaFormViewModel
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
        public async Task<IActionResult> Create(CategoriaFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                });
            }

            await _categoriaService.CriarCategoriaAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Categoria não encontrada.");
            }

            var categoria = await _categoriaService.FindByIdWithAuditAsync(id.Value);
            var vm = new EditarCategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Ativo = categoria.Ativo,
                DepartamentoId = categoria.DepartamentoId,

                Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                }),

                DataInsert = categoria.DataInsert,
                UsuarioInsertNome = categoria.UsuarioInsert?.DescNome,
                DataUpdate = categoria.DataUpdate,
                UsuarioUpdateNome = categoria.UsuarioUpdate?.DescNome,
                DataInativacao = categoria.DataInativacao,
                UsuarioInativacaoNome = categoria.UsuarioInativacao?.DescNome
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarCategoriaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Nome
                });
            }

            await _categoriaService.AlterarCategoriaAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Categoria não encontrada.");
            }

            var categoria = await _categoriaService.FindByIdAsync(id.Value);
            return View(categoria);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(int id)
        {
            await _categoriaService.InativarCategoriaAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Categoria não encontrada.");
            }

            var categoria = await _categoriaService.FindByIdAsync(id.Value);
            return View(categoria);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(int id)
        {
            await _categoriaService.AtivarCategoriaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
