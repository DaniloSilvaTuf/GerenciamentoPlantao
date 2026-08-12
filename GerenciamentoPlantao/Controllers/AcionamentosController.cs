using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using GerenciamentoPlantao.Constantes;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Data;
using Microsoft.EntityFrameworkCore;
using GerenciamentoPlantao.Exceptions;

namespace GerenciamentoPlantao.Controllers
{

    public class AcionamentosController : Controller
    {
        private readonly AcionamentoService _acionamentoService;
        private readonly CanalService _canalService;
        private readonly CategoriaService _categoriaService;
        private readonly EstabelecimentoService _estabelecimentoService;
        private readonly SetorService _setorService;
        private readonly SolucaoService _solucaoService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public AcionamentosController(AcionamentoService acionamentoService, CanalService canalService, CategoriaService categoriaService, EstabelecimentoService estabelecimentoService, SetorService setorService, SolucaoService solucaoService, IUsuarioLogadoService usuarioLogadoService)
        {
            _acionamentoService = acionamentoService;
            _canalService = canalService;
            _categoriaService = categoriaService;
            _estabelecimentoService = estabelecimentoService;
            _setorService = setorService;
            _solucaoService = solucaoService;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _acionamentoService.FindAllAsync();
            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            var vm = new AcionamentoFormViewModel
            {
                NomePlantonista = usuario.DescNome,
                Canal = _canalService.FindAllActiveAsync().Result.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                }),
                Estabelecimento = _estabelecimentoService.FindAllActiveAsync().Result.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                }),
                Setor = _setorService.FindAllActiveAsync().Result.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                }),
                Categoria = _categoriaService.FindAllActiveAsync().Result.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                }),
                Solucao = _solucaoService.FindAllActiveAsync().Result.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                })
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AcionamentoFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _acionamentoService.InserirAcionamentoAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Acionamento não encontrado.");
            }

            var acionamento = await _acionamentoService.FindByIdWithAuditAsync(id.Value);
            
            var vm = new EditarAcionamentoViewModel
            {
                Id = acionamento.Id,
                DataAcionamento = acionamento.DataAcionamento,
                NomePlantonista = acionamento.Plantonista.DescNome,
                UsuarioId = acionamento.UsuarioId,
                CanalId = acionamento.CanalId,
                Canal = (await _canalService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                }),
                Acionador = acionamento.Acionador,
                NrAtendimento = acionamento.NrAtendimento,
                EstabelecimentoId = acionamento.EstabelecimentoId,
                Estabelecimento = (await _estabelecimentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                }),
                SetorId = acionamento.SetorId,
                Setor = (await _setorService.FindAllActiveAsync()).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                }),
                CategoriaId = acionamento.CategoriaAcionamentoId,
                Categoria = (await _categoriaService.FindAllActiveAsync()).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                }),
                SolucaoId = acionamento.SolucaoId,
                Solucao = (await _solucaoService.FindAllActiveAsync()).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                }),
                Apoio = acionamento.Apoio,
                Observacao = acionamento.Observacao,
                
                DataInsert = acionamento.DataInsert,
                UsuarioInsertNome = acionamento.UsuarioInsert?.DescNome,
                DataUpdate = acionamento.DataUpdate,
                UsuarioUpdateNome = acionamento.UsuarioUpdate?.DescNome

            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarAcionamentoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
                vm.NomePlantonista = usuario?.DescNome ?? string.Empty;

                vm.Canal = (await _canalService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                });
                vm.Estabelecimento = (await _estabelecimentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                });
                vm.Setor = (await _setorService.FindAllActiveAsync()).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                });
                vm.Categoria = (await _categoriaService.FindAllActiveAsync()).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                });
                vm.Solucao = (await _solucaoService.FindAllActiveAsync()).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                });
            }

            await _acionamentoService.AlterarAcionamentoAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Acionamento não encontrado.");
            }

            var acionamento = await _acionamentoService.FindByIdAsync(id.Value);
            return View(acionamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _acionamentoService.RemoverAcionamentoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}