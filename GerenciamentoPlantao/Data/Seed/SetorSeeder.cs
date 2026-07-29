using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;
using GerenciamentoPlantao.Data;

namespace GerenciamentoPlantao.Data.Seed
{
    public static class SetorSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<GerenciamentoPlantaoContext>();

            if (await context.Setores.AnyAsync())
                return;

            var estabelecimento = await context.Estabelecimentos.FirstAsync(d => d.Nome == "Hospital SJP");

            var setor = new Setor
            {
                Nome = "Setor 1",
                Ativo = true,
                EstabelecimentoId = estabelecimento.Id
            };

            context.Setores.Add(setor);

            await context.SaveChangesAsync();
        }
    }
}
