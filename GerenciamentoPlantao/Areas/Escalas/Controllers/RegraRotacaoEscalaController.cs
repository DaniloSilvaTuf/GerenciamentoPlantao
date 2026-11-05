using GerenciamentoPlantao.Data;
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
    public class RegraRotacaoEscalaController : Controller
    {
        private readonly RegraRotacaoEscalaService _regraRotacaoEscalaService;
        private readonly RegraRotacaoHorarioEscalaService _regraRotacaoHorarioEscalaService;
        private readonly DepartamentoService _departamentoService;

        public RegraRotacaoEscalaController(RegraRotacaoEscalaService regraRotacaoEscalaService, RegraRotacaoHorarioEscalaService regraRotacaoHorarioEscalaService, DepartamentoService departamentoService)
        {
            _regraRotacaoEscalaService = regraRotacaoEscalaService;
            _regraRotacaoHorarioEscalaService = regraRotacaoHorarioEscalaService;
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int paginaAtual = 1, int tamanhoPagina = 10, string? busca = null, string? ordenarPor = null, string? direcao = null, int? departamentoId = null)
        {
            paginaAtual = Math.Max(1, paginaAtual);
            tamanhoPagina = Math.Clamp(tamanhoPagina, 1, 100);

            var lista = await _regraRotacaoEscalaService.FindAllAsync(paginaAtual, tamanhoPagina, busca, ordenarPor, direcao, departamentoId);

            ViewBag.Busca = busca;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.Direcao = direcao;
            ViewBag.DepartamentoId = departamentoId;
            ViewBag.TamanhoPagina = tamanhoPagina;

            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, int departamentoId)
        {
            try
            {
                var regra = await _regraRotacaoEscalaService
                    .FindByIdWithAuditAsync(id, departamentoId);

                var horarios = await _regraRotacaoHorarioEscalaService
                    .FindAllAsync(id);

                ViewBag.Horarios = horarios;

                return View(regra);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new RegraRotacaoEscalaViewModel
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
        public async Task<IActionResult> Create(RegraRotacaoEscalaViewModel vm)
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
            await _regraRotacaoEscalaService.InserirRegraRotacaoAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, int departamentoId)
        {
            try
            {
                var regra = await _regraRotacaoEscalaService
                    .FindByIdAsync(id, departamentoId);

                var vm = new EditarRegraRotacaoEscalaViewModel
                {
                    Id = regra.Id,
                    Nome = regra.Nome,
                    OrdemExecucao = regra.OrdemExecucao,
                    DepartamentoId = regra.DepartamentoId
                };

                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarRegraRotacaoEscalaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await _regraRotacaoEscalaService.AtualizarRegraRotacaoAsync(vm);

                TempData["SuccessMessage"] =
                    "Regra de rotação atualizada com sucesso.";

                return RedirectToAction(
                    nameof(Index),
                    new { departamentoId = vm.DepartamentoId });
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int departamentoId)
        {
            try
            {
                await _regraRotacaoEscalaService
                    .ExcluirRegraRotacaoAsync(id, departamentoId);

                TempData["SuccessMessage"] =
                    "Regra de rotação excluída com sucesso.";
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BusinessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(
                nameof(Index),
                new { departamentoId });
        }
    }
}