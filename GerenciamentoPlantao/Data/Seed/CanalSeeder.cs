using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Data.Seed
{
    public static class CanalSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<GerenciamentoPlantaoContext>();

            if (await context.Canais.AnyAsync())
                return;

            var departamento = await context.Departamentos.FirstAsync(d => d.Nome == "TI - Sistemas");

            var canal = new Canal
            {
                Nome = "Ligação",
                Ativo = true,
                DepartamentoId = departamento.Id
            };

            context.Canais.Add(canal);

            await context.SaveChangesAsync();
        }
    }
}
