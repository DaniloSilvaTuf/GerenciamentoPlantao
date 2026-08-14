using System.Security.Claims;

namespace GerenciamentoPlantao.Middleware
{
    public class TrocaSenhaObrigatoriaMiddleware
    {
        private readonly RequestDelegate _next;

        public TrocaSenhaObrigatoriaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var usuarioLogado = context.User;
            var estaAutenticado = usuarioLogado.Identity?.IsAuthenticated ?? false;
            
            if (!estaAutenticado)
            {
                await _next(context);

                return;
                
            }

            var precisaTrocarSenha = PrecisaTrocarSenha(usuarioLogado);

            var rotaPermitida = RotaPermitida(context.Request.Path);

            if(precisaTrocarSenha && !rotaPermitida)
            {
                context.Response.Redirect("/Usuarios/AlterarSenha");
                return;
            }

            await _next(context);
        }
        
        private bool PrecisaTrocarSenha(ClaimsPrincipal usuarioLogado)
        {
            var trocaSenhaObrigatoria = usuarioLogado.FindFirst("TrocaSenhaObrigatoria")?.Value;
            bool.TryParse(trocaSenhaObrigatoria, out bool precisaTrocarSenha);

            return precisaTrocarSenha;
        }

        private bool RotaPermitida(PathString rota)
        {
            return rota.StartsWithSegments("/Usuarios/AlterarSenha") || rota.StartsWithSegments("/Conta/Logout");

        }
    }
}
