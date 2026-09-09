using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Enums;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {

        private readonly UsuarioService _usuarioService;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly DepartamentoService _departamentoService;
        private readonly GerenciamentoPlantaoContext _context;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuariosController(UsuarioService usuarioService, DepartamentoService departamentoService, GerenciamentoPlantaoContext context, SignInManager<Usuario> signInManager, IUsuarioLogadoService usuarioLogadoService)
        {
            _usuarioService = usuarioService;
            _departamentoService = departamentoService;
            _context = context;
            _signInManager = signInManager;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<IActionResult> Index(string? ordenarPor, string? direcao, string? descNome, string? nmUsuario, int? departamentoId, int paginaAtual = 1)
        {
            var tamanhoPagina = 20;

            var lista = await _usuarioService.FindAllAsync(paginaAtual, tamanhoPagina, descNome, nmUsuario, ordenarPor, direcao, departamentoId);

            ViewBag.DescNome = descNome;
            ViewBag.NmUsuario = nmUsuario;
            ViewBag.DepartamentoId = departamentoId;
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.Direcao = direcao;

            ViewBag.Departamentos = await _departamentoService.FindAllActiveAsync();

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
                throw new NotFoundException("Usuário não encontrado.");
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

        public async Task<IActionResult> AlterarSenha(string? id)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var vm = new AlterarSenhaViewModel
            {
                UsuarioId = usuario.Id
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha (AlterarSenhaViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            await _usuarioService.AlterarSenhaAsync(vm);

            TempData["Sucesso"] = "Senha alterada com sucesso.";

            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Conta");
        }

        public async Task<IActionResult> AlterarSenhaAdm (string? id)
        {
            var vm = new AlterarSenhaAdmViewModel
            {
                UsuarioId = id
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenhaAdm (AlterarSenhaAdmViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            await _usuarioService.AlterarSenhaAdmAsync(vm);

            TempData["Sucesso"] = "Senha alterada com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Inativar(string? id)
        {
            if (id == null)
            {
                throw new NotFoundException("Usuário não encontrado.");
            }

            var usuario = await _usuarioService.FindByIdAsync(id);

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
                throw new NotFoundException("Usuário não encontrado.");
            }

            var usuario = await _usuarioService.FindByIdAsync(id);

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
