using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPlantao.Controllers
{
    public class ContaController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public ContaController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var usuario = await _userManager.FindByNameAsync(vm.Username);
            if (usuario == null || !usuario.Ativo)
            {
                ModelState.AddModelError("", "Nome de usuário ou senha inválidos.");
                return View(vm);
            }

            var resultado = await _signInManager.PasswordSignInAsync(
                vm.Username,
                vm.Password,
                vm.RememberMe,
                lockoutOnFailure: true);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");

            }

            if (resultado.IsLockedOut)
            {
                ModelState.AddModelError("", "Usuário bloqueado por tentativas de login incorretas.");
                return View(vm);
            }
            ModelState.AddModelError("", "Nome de usuário ou senha inválidos.");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}
