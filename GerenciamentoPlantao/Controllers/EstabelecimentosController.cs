using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Controllers
{
    public class EstabelecimentosController : Controller
    {

        public readonly EstabelecimentoService _estabelecimentoService;

        public EstabelecimentosController(EstabelecimentoService estabelecimentoService)
        {
            _estabelecimentoService = estabelecimentoService;
        }

        public  async Task<IActionResult> Index()
        {
            var lista = await _estabelecimentoService.FindAllAsync();
            return View(lista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estabelecimento estabelecimento)
        {
            if (!ModelState.IsValid) 
            {
                return View(estabelecimento);
            }

            await _estabelecimentoService.InsertAsync(estabelecimento);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimento = await _estabelecimentoService.FindByIdAsync(id.Value);
            
            if(estabelecimento == null)
            {
                return NotFound();
            }
            return View(estabelecimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Estabelecimento estabelecimento)
        {
            if (id != estabelecimento.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(estabelecimento);
            }

            await _estabelecimentoService.UpdateAsync(estabelecimento);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimento = await _estabelecimentoService.FindByIdAsync(id.Value);
            if (estabelecimento == null)
            {
                return NotFound();
            }

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
                return NotFound();
            }

            var estabelecimento = await _estabelecimentoService.FindByIdAsync(id.Value);

            if (estabelecimento == null)
            {
                return NotFound();
            }

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
