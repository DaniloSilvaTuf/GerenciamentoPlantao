using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Models.ViewModels.Escala;
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

        public PostoEscalaController(
            PostoEscalaService postoEscalaService,
            DepartamentoService departamentoService)
        {
            _postoEscalaService = postoEscalaService;
            _departamentoService = departamentoService;
        }

        // CARREGAR DEPARTAMENTOS
        private async Task<IEnumerable<SelectListItem>> CarregarDepartamentosAsync()
        {
            var departamentos = await _departamentoService
                .FindAllActiveAsync();

            return departamentos.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Nome
            }).ToList();
        }

        // CARREGAR DADOS DO POSTO PARA EDIÇÃO
        private async Task CarregarDadosEdicaoAsync(
    EditarPostoEscalaViewModel vm)
        {
            var posto = await _postoEscalaService
                .FindByIdWithAuditAsync(
                    vm.Id,
                    vm.DepartamentoId);

            // INFORMAÇÕES DO POSTO
            vm.DepartamentoNome = posto.Departamento.Nome;
            vm.Ativo = posto.Ativo;

            // AUDITORIA
            vm.DataInsert = posto.DataInsert;
            vm.UsuarioInsertNome = posto.UsuarioInsert?.DescNome;

            vm.DataUpdate = posto.DataUpdate;
            vm.UsuarioUpdateNome = posto.UsuarioUpdate?.DescNome;

            vm.DataInativacao = posto.DataInativacao;
            vm.UsuarioInativacaoNome =
                posto.UsuarioInativacao?.DescNome;

            // HORÁRIOS VINCULADOS
            var idsVinculados = await _postoEscalaService
                .ObterIdsHorariosVinculadosAsync(posto.Id);

            // Preserva os horários enviados no formulário.
            // Apenas atualiza a informação de vínculo.
            if (vm.Horarios != null)
            {
                foreach (var horario in vm.Horarios)
                {
                    horario.EstaVinculado =
                        horario.Id.HasValue &&
                        idsVinculados.Contains(horario.Id.Value);
                }
            }
        }

        // INDEX
        [HttpGet]
        public async Task<IActionResult> Index(
            string? ordenarPor,
            string? direcao,
            string? busca,
            int? departamentoId,
            int paginaAtual = 1)
        {
            const int tamanhoPagina = 20;

            paginaAtual = Math.Max(1, paginaAtual);

            var lista = await _postoEscalaService.FindAllAsync(
                paginaAtual,
                tamanhoPagina,
                busca,
                ordenarPor,
                direcao,
                departamentoId);

            ViewBag.Busca = busca;
            ViewBag.Direcao = direcao;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.DepartamentoId = departamentoId;

            ViewBag.Departamentos = await _departamentoService
                .FindAllActiveAsync();

            return View(lista);
        }

        // CREATE - GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new PostoEscalaFormViewModel
            {
                Departamentos = await CarregarDepartamentosAsync(),

                Horarios = new List<HorarioPostoMultiploDiasViewModel>
                    {
                        new HorarioPostoMultiploDiasViewModel()
                    }
            };

            return View(vm);
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            PostoEscalaFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departamentos = await CarregarDepartamentosAsync();

                return View(vm);
            }

            try
            {
                await _postoEscalaService.CriarPostoAsync(vm);

                TempData["SuccessMessage"] =
                    "Posto e horários cadastrados com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);
            }

            vm.Departamentos = await CarregarDepartamentosAsync();

            return View(vm);
        }

        // EDIT - GET
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
                var posto = await _postoEscalaService
                    .FindByIdWithAuditAsync(id.Value, departamentoId.Value);

                var idsVinculados = await _postoEscalaService
                    .ObterIdsHorariosVinculadosAsync(posto.Id);

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
                    UsuarioInativacaoNome =
                        posto.UsuarioInativacao?.DescNome,

                    Horarios = posto.Horarios
                        .OrderBy(h => h.DiaSemana)
                        .ThenBy(h => h.HoraInicio)
                        .Select(h => new BlocoHorarioPostoViewModel
                        {
                            Id = h.Id,
                            Nome = h.Nome,
                            HoraInicio = h.HoraInicio,
                            HoraFim = h.HoraFim,
                            DiaSemana = h.DiaSemana,
                            EstaVinculado = idsVinculados.Contains(h.Id)
                        })
                        .ToList(),
                };

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
            EditarPostoEscalaViewModel vm)
        {
            try
            {
                // Informações que não devem ser alteradas
                // diretamente pelo formulário.
                await CarregarDadosEdicaoAsync(vm);

                // Evita erros de validação em campos
                // preenchidos exclusivamente pelo servidor.
                ModelState.Remove(nameof(vm.DepartamentoNome));

                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                await _postoEscalaService.AtualizarPostoAsync(vm);

                TempData["SuccessMessage"] =
                    "Posto e horários atualizados com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                return View(vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // INATIVAR - GET
        [HttpGet]
        public async Task<IActionResult> Inativar(
            int? id,
            int? departamentoId)
        {
            if (!id.HasValue || !departamentoId.HasValue)
            {
                return NotFound();
            }

            try
            {
                var posto = await _postoEscalaService.FindByIdAsync(
                    id.Value,
                    departamentoId.Value);

                return View(posto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // INATIVAR - POST
        [HttpPost]
        [ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(
            int id,
            int departamentoId)
        {
            try
            {
                await _postoEscalaService.InativarPostoAsync(
                    id,
                    departamentoId);

                TempData["SuccessMessage"] =
                    "Posto inativado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BusinessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        // ATIVAR - GET
        [HttpGet]
        public async Task<IActionResult> Ativar(
            int? id,
            int? departamentoId)
        {
            if (!id.HasValue || !departamentoId.HasValue)
            {
                return NotFound();
            }

            try
            {
                var posto = await _postoEscalaService.FindByIdAsync(
                    id.Value,
                    departamentoId.Value);

                return View(posto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // ATIVAR - POST
        [HttpPost]
        [ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(
            int id,
            int departamentoId)
        {
            try
            {
                await _postoEscalaService.AtivarPostoAsync(
                    id,
                    departamentoId);

                TempData["SuccessMessage"] =
                    "Posto ativado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BusinessException ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}