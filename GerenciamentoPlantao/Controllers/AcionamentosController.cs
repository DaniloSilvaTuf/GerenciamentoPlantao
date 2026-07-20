using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly UsuarioService _usuarioService;

        public AcionamentosController(AcionamentoService acionamentoService, CanalService canalService, CategoriaService categoriaService, EstabelecimentoService estabelecimentoService, SetorService setorService, SolucaoService solucaoService, UsuarioService usuarioService)
        {
            _acionamentoService = acionamentoService;
            _canalService = canalService;
            _categoriaService = categoriaService;
            _estabelecimentoService = estabelecimentoService;
            _setorService = setorService;
            _solucaoService = solucaoService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _acionamentoService.FindAllAsync();
            return View(lista);
        }

        public IActionResult Create()
        {
            var vm = new AcionamentoFormViewModel
            {
                Plantonista = _usuarioService.FindAllActiveAsync().Result.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.DescNome
                }),
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

            await _acionamentoService.InsertAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acionamento = await _acionamentoService.FindByIdAsync(id.Value);
            var vm = new EditarAcionamentoViewModel
            {
                Id = acionamento.Id,
                DataAcionamento = acionamento.DataAcionamento,
                UsuarioId = acionamento.UsuarioId,
                Plantonista = (await _usuarioService.FindAllActiveAsync()).Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.DescNome
                }),
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
                Observacao = acionamento.Observacao
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarAcionamentoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Plantonista = (await _usuarioService.FindAllActiveAsync()).Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.NmUsuario
                });
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

            await _acionamentoService.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acionamento = await _acionamentoService.FindByIdAsync(id.Value);

            if (acionamento == null)
            {
                return NotFound();
            }

            return View(acionamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _acionamentoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}