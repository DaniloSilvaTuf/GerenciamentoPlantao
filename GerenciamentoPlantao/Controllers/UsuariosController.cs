using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Enums;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {

        private readonly UsuarioService _usuarioService;
        private readonly DepartamentoService _departamentoService;
        private readonly GerenciamentoPlantaoContext _context;

        public UsuariosController(UsuarioService usuarioService, DepartamentoService departamentoService, GerenciamentoPlantaoContext context)
        {
            _usuarioService = usuarioService;
            _departamentoService = departamentoService;
            _context = context;
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
                }),

                Perfis = Enum.GetValues<PerfilUsuario>().Select(p => new SelectListItem
                {
                    Value = p.ToString(),
                    Text = p.ToString()
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
                vm.Perfis = Enum.GetValues<PerfilUsuario>().Select(p => new SelectListItem
                {
                    Value = p.ToString(),
                    Text = p.ToString()
                });
                return View(vm);
            }

            await _usuarioService.CriarUsuarioAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _usuarioService.FindByIdWithAuditAsync(id);

            var vm = new EditarUsuarioViewModel
            {
                Id = usuario.Id,
                DescNome = usuario.DescNome,
                NmUsuario = usuario.UserName,
                DepartamentoId = usuario.DepartamentoId,
                Email = usuario.Email,
                Telefone = usuario.PhoneNumber,
                Plantonista = usuario.Plantonista,
                Ativo = usuario.Ativo,
                Departamentos = (await _departamentoService.FindAllActiveAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                }),

                DataInsert = usuario.DataInsert,
                UsuarioInsertNome = usuario.UsuarioInsert?.DescNome,
                DataUpdate = usuario.DataUpdate,
                UsuarioUpdateNome = usuario.UsuarioUpdate?.DescNome,
                DataInativacao = usuario.DataInativacao,
                UsuarioInativacaoNome = usuario.UsuarioInativacao?.DescNome,

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

            await _usuarioService.AlterarUsuarioAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _usuarioService.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InativarConfirmado(string id)
        {
            await _usuarioService.InativarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ativar(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _usuarioService.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Ativar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtivarConfirmado(string id)
        {
            await _usuarioService.AtivarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
