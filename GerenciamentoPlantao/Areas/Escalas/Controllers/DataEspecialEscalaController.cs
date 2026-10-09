using GerenciamentoPlantao.Exceptions;
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
    public class DataEspecialEscalaController : Controller
    {

        private readonly DataEspecialEscalaService _dataEspecialEscalaService;
        private readonly DepartamentoService _departamentoService;

        public DataEspecialEscalaController(DataEspecialEscalaService dataEspecialEscalaService, DepartamentoService departamentoService)
        {
            _dataEspecialEscalaService = dataEspecialEscalaService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index(string? ordenarPor, string? direcao, string? busca, int? departamentoId, int paginaAtual = 1)
        {
            var tamanhoPagina = 20;

            var lista = await _dataEspecialEscalaService.FindAllAsync(paginaAtual, tamanhoPagina, busca, ordenarPor, direcao, departamentoId);

            ViewBag.Busca = busca;
            ViewBag.Direcao = direcao;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.DepartamentoId = departamentoId;

            ViewBag.Departamentos = await _departamentoService.FindAllActiveAsync();

            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new DataEspecialEscalaFormViewModel
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
        public async Task<IActionResult> Create(DataEspecialEscalaFormViewModel vm)
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

            await _dataEspecialEscalaService.CriarDataEspecialEscalaAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id, int? departamentoId)
        {
            if (!id.HasValue || !departamentoId.HasValue)
            {
                throw new NotFoundException("Data especial escala não encontrada.");
            }

            var dataEspecial = await _dataEspecialEscalaService.FindByIdWithAuditAsync(id.Value, departamentoId.Value);
            var vm = new EditarDataEspecialFormViewModel
            {
                Id = dataEspecial.Id,
                Descricao = dataEspecial.Descricao,
                NovaData = dataEspecial.Data,
                DepartamentoId = dataEspecial.DepartamentoId,
                DepartamentoNome = dataEspecial.Departamento.Nome,
                
                DataInsert = dataEspecial.DataInsert,
                UsuarioInsertNome = dataEspecial.UsuarioInsert?.DescNome,
                DataUpdate = dataEspecial.DataUpdate,
                UsuarioUpdateNome = dataEspecial.UsuarioUpdate?.DescNome,
                DataInativacao = dataEspecial.DataInativacao,
                UsuarioInativacaoNome = dataEspecial.UsuarioInativacao?.DescNome
            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarDataEspecialFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {

                var dataEspecial = await _dataEspecialEscalaService.FindByIdAsync(vm.Id, vm.DepartamentoId);
                vm.DepartamentoNome = dataEspecial.Departamento.Nome;
                return View(vm);
            }
            await _dataEspecialEscalaService.AtualizarDataEspecialEscalaAsync(vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
