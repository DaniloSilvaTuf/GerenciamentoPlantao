using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Services.Escalas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Areas.Escalas.Controllers
{
    [Area("Escalas")]
    [Authorize]
    public class RegraRotacaoHorarioEscalaController : Controller
    {
        private readonly RegraRotacaoHorarioEscalaService _horarioService;
        private readonly RegraRotacaoEscalaService _regraService;
        private readonly GerenciamentoPlantaoContext _context;

        public RegraRotacaoHorarioEscalaController(
            RegraRotacaoHorarioEscalaService horarioService,
            RegraRotacaoEscalaService regraService,
            GerenciamentoPlantaoContext context)
        {
            _horarioService = horarioService;
            _regraService = regraService;
            _context = context;
        }

        // Carrega os horários pertencentes aos postos do departamento.
        private async Task CarregarBlocosHorarioAsync(int departamentoId)
        {
            var blocos = await _context.BlocosHorarioEscala
                .AsNoTracking()
                .Where(b => b.PostoEscala.DepartamentoId == departamentoId)
                .OrderBy(b => b.PostoEscala.Nome)
                .ThenBy(b => b.DiaSemana)
                .ThenBy(b => b.HoraInicio)
                .Select(b => new
                {
                    b.Id,
                    PostoNome = b.PostoEscala.Nome,
                    b.Nome,
                    b.DiaSemana,
                    b.HoraInicio,
                    b.HoraFim
                })
                .ToListAsync();

            var opcoes = blocos.Select(b => new
            {
                b.Id,
                Descricao = $"{b.PostoNome} | {b.Nome} | " +
                            $"{b.DiaSemana} | " +
                            $"{b.HoraInicio:HH:mm} às {b.HoraFim:HH:mm}"
            });

            ViewBag.BlocosHorario = new SelectList(
                opcoes,
                "Id",
                "Descricao"
            );
        }

        // CREATE - GET
        [HttpGet]
        public async Task<IActionResult> Create(
            int regraRotacaoId,
            int departamentoId)
        {
            try
            {
                await _regraService.FindByIdAsync(
                    regraRotacaoId,
                    departamentoId);

                await CarregarBlocosHorarioAsync(departamentoId);

                var vm = new RegraRotacaoHorarioEscalaViewModel
                {
                    RegraRotacaoId = regraRotacaoId
                };

                ViewBag.DepartamentoId = departamentoId;

                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            RegraRotacaoHorarioEscalaViewModel vm,
            int departamentoId)
        {
            try
            {
                await _regraService.FindByIdAsync(
                    vm.RegraRotacaoId,
                    departamentoId);

                if (vm.OrdemRotacao <= 0)
                {
                    ModelState.AddModelError(
                        nameof(vm.OrdemRotacao),
                        "A ordem de rotação deve ser maior que zero.");
                }

                if (vm.BlocoHorarioEscalaId <= 0)
                {
                    ModelState.AddModelError(
                        nameof(vm.BlocoHorarioEscalaId),
                        "Selecione um horário.");
                }

                if (!ModelState.IsValid)
                {
                    await CarregarBlocosHorarioAsync(departamentoId);

                    ViewBag.DepartamentoId = departamentoId;

                    return View(vm);
                }

                await _horarioService
                    .CriarRegraRotacaoHorarioEscalaAsync(vm);

                TempData["SuccessMessage"] =
                    "Horário cadastrado com sucesso.";

                return RedirectToAction(
                    "Details",
                    "RegraRotacaoEscala",
                    new
                    {
                        id = vm.RegraRotacaoId,
                        departamentoId
                    });
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                await CarregarBlocosHorarioAsync(departamentoId);

                ViewBag.DepartamentoId = departamentoId;

                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // EDIT - GET
        [HttpGet]
        public async Task<IActionResult> Edit(
            int id,
            int regraRotacaoId,
            int departamentoId)
        {
            try
            {
                await _regraService.FindByIdAsync(
                    regraRotacaoId,
                    departamentoId);

                var horario = await _horarioService.FindByIdAsync(
                    id,
                    regraRotacaoId);

                var vm = new EditarRegraRotacaoHorarioEscalaViewModel
                {
                    Id = horario.Id,
                    RegraRotacaoId = horario.RegraRotacaoId,
                    OrdemRotacao = horario.OrdemRotacao,
                    BlocoHorarioEscalaId = horario.BlocoHorarioEscalaId
                };

                await CarregarBlocosHorarioAsync(departamentoId);

                ViewBag.DepartamentoId = departamentoId;

                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            EditarRegraRotacaoHorarioEscalaViewModel vm,
            int departamentoId)
        {
            try
            {
                await _regraService.FindByIdAsync(
                    vm.RegraRotacaoId,
                    departamentoId);

                if (vm.OrdemRotacao <= 0)
                {
                    ModelState.AddModelError(
                        nameof(vm.OrdemRotacao),
                        "A ordem de rotação deve ser maior que zero.");
                }

                if (vm.BlocoHorarioEscalaId <= 0)
                {
                    ModelState.AddModelError(
                        nameof(vm.BlocoHorarioEscalaId),
                        "Selecione um horário.");
                }

                if (!ModelState.IsValid)
                {
                    await CarregarBlocosHorarioAsync(departamentoId);

                    ViewBag.DepartamentoId = departamentoId;

                    return View(vm);
                }

                await _horarioService
                    .AtualizarRegraRotacaoHorarioEscalaAsync(vm);

                TempData["SuccessMessage"] =
                    "Horário atualizado com sucesso.";

                return RedirectToAction(
                    "Details",
                    "RegraRotacaoEscala",
                    new
                    {
                        id = vm.RegraRotacaoId,
                        departamentoId
                    });
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                await CarregarBlocosHorarioAsync(departamentoId);

                ViewBag.DepartamentoId = departamentoId;

                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(
            int id,
            int regraRotacaoId,
            int departamentoId)
        {
            try
            {
                await _regraService.FindByIdAsync(
                    regraRotacaoId,
                    departamentoId);

                await _horarioService
                    .ExcluirRegraRotacaoHorarioEscalaAsync(
                        id,
                        regraRotacaoId);

                TempData["SuccessMessage"] =
                    "Horário excluído com sucesso.";

                return RedirectToAction(
                    "Details",
                    "RegraRotacaoEscala",
                    new
                    {
                        id = regraRotacaoId,
                        departamentoId
                    });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BusinessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return RedirectToAction(
                    "Details",
                    "RegraRotacaoEscala",
                    new
                    {
                        id = regraRotacaoId,
                        departamentoId
                    });
            }
        }
    }
}