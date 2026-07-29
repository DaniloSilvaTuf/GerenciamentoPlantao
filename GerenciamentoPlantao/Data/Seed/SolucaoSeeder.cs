using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Data.Seed
{
    public static class SolucaoSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<GerenciamentoPlantaoContext>();

            if (await context.Solucoes.AnyAsync())
                return;

            var departamento = await context.Departamentos.FirstAsync(d => d.Nome == "TI - Sistemas");

            var solucao = new Solucao
            {
                Nome = "Auxílio no processo",
                Ativo = true,
                DepartamentoId = departamento.Id
            };

            context.Solucoes.Add(solucao);

            await context.SaveChangesAsync();
        }
    }
}
