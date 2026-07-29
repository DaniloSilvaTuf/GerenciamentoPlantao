using GerenciamentoPlantao.Constantes;
using Microsoft.AspNetCore.Identity;

namespace GerenciamentoPlantao.Data.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles =
            {
                Roles.Administrador,
                Roles.Consulta,
                Roles.Plantonista
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}