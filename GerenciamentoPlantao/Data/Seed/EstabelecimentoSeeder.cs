using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Data.Seed
{
    public static class EstabelecimentoSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<GerenciamentoPlantaoContext>();

            if (await context.Estabelecimentos.AnyAsync())
                return;

            var estabelecimento = new Estabelecimento
            {
                Nome = "Hospital SJP",
                Ativo = true
            };

            context.Estabelecimentos.Add(estabelecimento);

            await context.SaveChangesAsync();
        }
    }
}
