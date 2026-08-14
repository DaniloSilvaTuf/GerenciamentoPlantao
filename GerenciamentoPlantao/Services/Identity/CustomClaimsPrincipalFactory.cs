using GerenciamentoPlantao.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace GerenciamentoPlantao.Services.Identity
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<Usuario, IdentityRole>
    {
        public CustomClaimsPrincipalFactory(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Usuario usuario)
        {
            var identity = await base.GenerateClaimsAsync(usuario);

            identity.AddClaim(new Claim("DescNome", usuario.DescNome));
            identity.AddClaim(new Claim("Perfil", usuario.Perfil.ToString()));
            identity.AddClaim(new Claim("TrocaSenhaObrigatoria", usuario.TrocaSenhaObrigatoria.ToString()));

            return identity;
        }
    }
}