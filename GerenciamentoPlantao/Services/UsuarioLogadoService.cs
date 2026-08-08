using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GerenciamentoPlantao.Services
{
    public class UsuarioLogadoService : IUsuarioLogadoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioLogadoService(IHttpContextAccessor httpContextAccessor, UserManager<Usuario> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public async Task<bool> EstaAutenticadoAsync()
        {
            return User?.Identity?.IsAuthenticated ?? false;
        }

        public async Task<Usuario> ObterUsuarioLogadoAsync()
        {
            var usuario = await _userManager.GetUserAsync(User!);

            if (usuario == null)
            {
                throw new Exception("Usuário logado não encontrado.");
            }

            return usuario;
        }

        public async Task<string?> ObterIdUsuarioAsync()
        {
            var usuario = await ObterUsuarioLogadoAsync();
            return usuario?.Id;
        }

        public async Task<string?> ObterNomeUsuarioLogadoAsync()
        {
            var usuario = await ObterUsuarioLogadoAsync();
            return usuario?.DescNome;
        }

        public async Task<int?> ObterDepartamentoIdAsync()
        {
            var usuario = await ObterUsuarioLogadoAsync();
            return usuario?.DepartamentoId;
        }

        public async Task<PerfilUsuario?> ObterPerfilUsuarioLogadoAsync()
        {
            var usuario = await ObterUsuarioLogadoAsync();
            return usuario?.Perfil;
        }
    }
}
