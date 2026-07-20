using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly UsuarioService _usuarioService;
        private readonly DepartamentoService _departamentoService;

        public UsuariosController(UsuarioService usuarioService, DepartamentoService departamentoService)
        {
            _usuarioService = usuarioService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _usuarioService.FindAllAsync();
            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new UsuarioFormViewModel
            {
                Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                })
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                });
                return View(vm);
            }

            await _usuarioService.InsertAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _usuarioService.FindByIdAsync(id.Value);
            var vm = new EditarUsuarioViewModel
            {
                Id = usuario.Id,
                DescNome = usuario.DescNome,
                NmUsuario = usuario.NmUsuario,
                DepartamentoId = usuario.DepartamentoId,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Plantonista = usuario.Plantonista,
                Ativo = usuario.Ativo,
                Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                })
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarUsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                });
            }

            await _usuarioService.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _usuarioService.FindByIdAsync(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(int id)
        {
            await _usuarioService.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _usuarioService.FindByIdAsync(id.Value);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(int id)
        {
            await _usuarioService.AtivarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
