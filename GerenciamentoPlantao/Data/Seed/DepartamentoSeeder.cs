using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Data.Seed
{
    public static class DepartamentoSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<GerenciamentoPlantaoContext>();

            if (await context.Departamentos.AnyAsync())
                return;

            var departamento = new Departamento
            {
                Nome = "TI - Sistemas",
                Ativo = true
            };

            context.Departamentos.Add(departamento);

            await context.SaveChangesAsync();
        }
    }
}