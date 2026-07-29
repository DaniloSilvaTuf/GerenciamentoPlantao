using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Data.Seed
{
    public static class CategoriaSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<GerenciamentoPlantaoContext>();

            if (await context.CategoriasAcionamento.AnyAsync())
                return;

            var departamento = await context.Departamentos.FirstAsync(d => d.Nome == "TI - Sistemas");

            var categoria = new CategoriaAcionamento
            {
                Nome = "Pendência de Assinatura",
                Ativo = true,
                DepartamentoId = departamento.Id
            };

            context.CategoriasAcionamento.Add(categoria);

            await context.SaveChangesAsync();
        }
    }
}
