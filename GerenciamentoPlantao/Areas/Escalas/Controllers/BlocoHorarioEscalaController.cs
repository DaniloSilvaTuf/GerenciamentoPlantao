using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Enums;
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
    public class BlocoHorarioEscalaController : Controller
    {
        private readonly BlocoHorarioEscalaService _blocoHorarioEscalaService;
        private readonly DepartamentoService _departamentoService;

        public BlocoHorarioEscalaController(BlocoHorarioEscalaService blocoHorarioEscalaService, DepartamentoService departamentoService)
        {
            _blocoHorarioEscalaService = blocoHorarioEscalaService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index(string? ordenarPor, string? direcao, string? busca, int? departamentoId, TipoDiaSemana? diaSemana, int paginaAtual = 1)
        {
            var tamanhoPagina = 20;

            var lista = await _blocoHorarioEscalaService.FindAllAsync(paginaAtual, tamanhoPagina, busca, ordenarPor, direcao, departamentoId, diaSemana);

            ViewBag.Busca = busca;
            ViewBag.Direcao = direcao;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.DepartamentoId = departamentoId;
            ViewBag.DiaSemana = diaSemana;

            ViewBag.Departamentos = await _departamentoService.FindAllActiveAsync();

            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new BlocoHorarioEscalaViewModel
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
        public async Task<IActionResult> Create(BlocoHorarioEscalaViewModel vm)
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

            await _blocoHorarioEscalaService.InserirBlocoHorarioAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id, int? departamentoId)
        {
            if (!id.HasValue || !departamentoId.HasValue)
            {
                throw new NotFoundException("Bloco horário escala não encontrado.");
            }

            var bloco = await _blocoHorarioEscalaService.FindByIdWithAuditAsync(id.Value, departamentoId.Value);
            var vm = new EditarBlocoHorarioEscalaViewModel
            {
                Id = bloco.Id,
                Nome = bloco.Nome,
                Ativo = bloco.Ativo,
                HoraInicio = bloco.HoraInicio,
                HoraFim = bloco.HoraFim,
                DiaSemana = bloco.DiaSemana,
                DepartamentoId = bloco.DepartamentoId,
                DepartamentoNome = bloco.Departamento.Nome,
                DataInsert = bloco.DataInsert,
                UsuarioInsertNome = bloco.UsuarioInsert?.DescNome,
                DataUpdate = bloco.DataUpdate,
                UsuarioUpdateNome = bloco.UsuarioUpdate?.DescNome,
                DataInativacao = bloco.DataInativacao,
                UsuarioInativacaoNome = bloco.UsuarioInativacao?.DescNome
            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarBlocoHorarioEscalaViewModel vm)
        {
            if (!ModelState.IsValid)
            {

                var bloco = await _blocoHorarioEscalaService.FindByIdAsync(vm.Id, vm.DepartamentoId);
                vm.DepartamentoNome = bloco.Departamento.Nome;
                vm.Ativo = bloco.Ativo;
                return View(vm);
            }
            await _blocoHorarioEscalaService.AtualizarBlocoHorarioAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inativar(int id, int departamentoId)
        {
            await _blocoHorarioEscalaService
                .InativarBlocoHorarioEscalaAsync(id, departamentoId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ativar(int id, int departamentoId)
        {
            await _blocoHorarioEscalaService
                .AtivarBlocoHorarioEscalaAsync(id, departamentoId);

            return RedirectToAction(nameof(Index));
        }
    }
}
