using GerenciamentoPlantao.Constantes;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Data.Seed
{
    public static class UsuarioSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<Usuario>>();
            var context = services.GetRequiredService<GerenciamentoPlantaoContext>();

            // Verifica se o admin já existe
            var usuario = await userManager.FindByNameAsync("admin");

            if (usuario != null)
                return;

            var departamento = await context.Departamentos.FirstAsync(d => d.Nome == "TI - Sistemas");

            usuario = new Usuario
            {
                UserName = "admin",
                DescNome = "Administrador",
                Email = "admin@tuftech.com.br",
                PhoneNumber = "(17)99999-9999",
                DepartamentoId = departamento.Id,
                Plantonista = false,
                Ativo = true,
                Perfil = PerfilUsuario.Administrador
            };

            var resultado = await userManager.CreateAsync(usuario, "Adm@2026");

            if (!resultado.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine,
                        resultado.Errors.Select(e => e.Description)));
            }

            await userManager.AddToRoleAsync(usuario, Roles.Administrador);
        }
    }
}