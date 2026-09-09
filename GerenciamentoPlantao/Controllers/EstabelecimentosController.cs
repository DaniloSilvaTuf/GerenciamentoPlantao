using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Exceptions;

namespace GerenciamentoPlantao.Controllers
{
    [Authorize]
    public class EstabelecimentosController : Controller
    {

        public readonly EstabelecimentoService _estabelecimentoService;

        public EstabelecimentosController(EstabelecimentoService estabelecimentoService)
        {
            _estabelecimentoService = estabelecimentoService;
        }

        public  async Task<IActionResult> Index(string? ordenarPor, string? direcao, string? busca, int paginaAtual = 1)
        {
            var tamanhoPagina = 20;

            var lista = await _estabelecimentoService.FindAllAsync(paginaAtual, tamanhoPagina, busca, ordenarPor, direcao);

            ViewBag.Busca = busca;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.Direcao = direcao;

            return View(lista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstabelecimentoFormViewModel vm)
        {
            if (!ModelState.IsValid) 
            {
                return View(vm);
            }

            await _estabelecimentoService.InserirEstabelecimentoAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Estabelecimento não encontrado.");
            }

            var estabelecimento = await _estabelecimentoService.FindByIdWithAuditAsync(id.Value);
            var vm = new EditarEstabelecimentoViewModel
            {
                Id = estabelecimento.Id,
                Nome = estabelecimento.Nome,
                Ativo = estabelecimento.Ativo,
                DataInsert = estabelecimento.DataInsert,
                UsuarioInsertNome = estabelecimento.UsuarioInsert?.DescNome,
                DataUpdate = estabelecimento.DataUpdate,
                UsuarioUpdateNome = estabelecimento.UsuarioUpdate?.DescNome,
                DataInativacao = estabelecimento.DataInativacao,
                UsuarioInativacaoNome = estabelecimento.UsuarioInativacao?.DescNome,
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarEstabelecimentoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _estabelecimentoService.AlterarEstabelecimentoAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Estabelecimento não encontrado.");
            }

            var estabelecimento = await _estabelecimentoService.FindByIdAsync(id.Value);
            return View(estabelecimento);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(int id)
        {
            await _estabelecimentoService.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(int? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Estabelecimento não encontrado.");
            }

            var estabelecimento = await _estabelecimentoService.FindByIdAsync(id.Value);
            return View(estabelecimento);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(int id)
        {
            await _estabelecimentoService.AtivarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
