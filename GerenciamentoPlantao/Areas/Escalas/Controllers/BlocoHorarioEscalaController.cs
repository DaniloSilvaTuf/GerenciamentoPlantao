using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Enums;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Services;
using GerenciamentoPlantao.Services.Escalas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Areas.Escalas.Controllers
{
    [Area("Escalas")]
    [Authorize]
    public class BlocoHorarioEscalaController : Controller
    {
        private readonly BlocoHorarioEscalaService _blocoHorarioEscalaService;
        private readonly DepartamentoService _departamentoService;
        private readonly GerenciamentoPlantaoContext _context;

        public BlocoHorarioEscalaController(
            BlocoHorarioEscalaService blocoHorarioEscalaService,
            DepartamentoService departamentoService,
            GerenciamentoPlantaoContext context)
        {
            _blocoHorarioEscalaService = blocoHorarioEscalaService;
            _departamentoService = departamentoService;
            _context = context;
        }

        private async Task CarregarPostosAsync()
        {
            var postos = await _context.PostosEscalas
                .AsNoTracking()
                .Include(p => p.Departamento)
                .Where(p => p.Ativo && p.Departamento.Ativo)
                .OrderBy(p => p.Departamento.Nome)
                .ThenBy(p => p.Nome)
                .Select(p => new
                {
                    p.Id,
                    Nome = p.Departamento.Nome + " - " + p.Nome
                })
                .ToListAsync();

            ViewBag.Postos = new SelectList(
                postos,
                "Id",
                "Nome");
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string? ordenarPor,
            string? direcao,
            string? busca,
            int? departamentoId,
            TipoDiaSemana? diaSemana,
            int paginaAtual = 1)
        {
            const int tamanhoPagina = 20;

            paginaAtual = Math.Max(1, paginaAtual);

            var lista = await _blocoHorarioEscalaService.FindAllAsync(
                paginaAtual,
                tamanhoPagina,
                busca,
                ordenarPor,
                direcao,
                departamentoId,
                diaSemana);

            ViewBag.Busca = busca;
            ViewBag.Direcao = direcao;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.DepartamentoId = departamentoId;
            ViewBag.DiaSemana = diaSemana;

            ViewBag.Departamentos =
                await _departamentoService.FindAllActiveAsync();

            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new BlocoHorarioEscalaViewModel();

            await CarregarPostosAsync();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            BlocoHorarioEscalaViewModel vm)
        {
            if (vm.PostoEscalaId <= 0)
            {
                ModelState.AddModelError(
                    nameof(vm.PostoEscalaId),
                    "Selecione um posto de escala.");
            }

            if (!ModelState.IsValid)
            {
                await CarregarPostosAsync();
                return View(vm);
            }

            try
            {
                await _blocoHorarioEscalaService
                    .InserirBlocoHorarioAsync(vm);

                TempData["SuccessMessage"] =
                    "Horário cadastrado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                await CarregarPostosAsync();

                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(
            int? id,
            int? departamentoId)
        {
            if (!id.HasValue || !departamentoId.HasValue)
            {
                return NotFound();
            }

            try
            {
                var bloco = await _blocoHorarioEscalaService
                    .FindByIdWithAuditAsync(
                        id.Value,
                        departamentoId.Value);

                var vm = new EditarBlocoHorarioEscalaViewModel
                {
                    Id = bloco.Id,
                    Nome = bloco.Nome,
                    Ativo = bloco.Ativo,
                    HoraInicio = bloco.HoraInicio,
                    HoraFim = bloco.HoraFim,
                    DiaSemana = bloco.DiaSemana,

                    DepartamentoId =
                        bloco.PostoEscala.DepartamentoId,

                    DepartamentoNome =
                        bloco.PostoEscala.Departamento.Nome,

                    DataInsert = bloco.DataInsert,
                    UsuarioInsertNome = bloco.UsuarioInsert?.DescNome,

                    DataUpdate = bloco.DataUpdate,
                    UsuarioUpdateNome = bloco.UsuarioUpdate?.DescNome,

                    DataInativacao = bloco.DataInativacao,
                    UsuarioInativacaoNome =
                        bloco.UsuarioInativacao?.DescNome
                };

                ViewBag.PostoNome = bloco.PostoEscala.Nome;

                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            EditarBlocoHorarioEscalaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var bloco = await _blocoHorarioEscalaService
                        .FindByIdAsync(vm.Id, vm.DepartamentoId);

                    vm.DepartamentoNome =
                        bloco.PostoEscala.Departamento.Nome;

                    vm.Ativo = bloco.Ativo;

                    ViewBag.PostoNome = bloco.PostoEscala.Nome;

                    return View(vm);
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }
            }

            try
            {
                await _blocoHorarioEscalaService
                    .AtualizarBlocoHorarioAsync(vm);

                TempData["SuccessMessage"] =
                    "Horário atualizado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                try
                {
                    var bloco = await _blocoHorarioEscalaService
                        .FindByIdAsync(vm.Id, vm.DepartamentoId);

                    vm.DepartamentoNome =
                        bloco.PostoEscala.Departamento.Nome;

                    vm.Ativo = bloco.Ativo;

                    ViewBag.PostoNome = bloco.PostoEscala.Nome;

                    return View(vm);
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(
            int id,
            int departamentoId)
        {
            try
            {
                await _blocoHorarioEscalaService
                    .ExcluirBlocoHorarioAsync(id, departamentoId);

                TempData["SuccessMessage"] =
                    "Horário excluído com sucesso.";
            }
            catch (BusinessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}