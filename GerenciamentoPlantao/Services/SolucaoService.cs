using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class SolucaoService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public SolucaoService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }

        public async Task<List<Solucao>> FindAllAsync()
        {
            return await _context.Solucoes.Include(s => s.Departamento).ToListAsync();
        }

        public async Task<Solucao?> FindByIdAsync(int id)
        {
            return await _context.Solucoes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CriarSolucaoAsync(int departamentoId, string nome)
        {
            var departamento = await _context.Departamentos
                    .Include(d => d.Solucoes)
                    .FirstOrDefaultAsync(d => d.Id == departamentoId);

            if (departamento == null)
                throw new Exception("Departamento não encontrado.");

            var solucao = new Solucao(nome, departamentoId);
            solucao.Ativar();
            departamento.AddSolucao(solucao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditarSolucaoViewModel vm)
        {
            var solucao = await FindByIdAsync(vm.Id);


            if (solucao == null)
            {
                throw new Exception("Solução não encontrada");
            }

            solucao.Nome = vm.Nome;
            solucao.DepartamentoId = vm.DepartamentoId;
            _context.Update(solucao);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var solucao = await FindByIdAsync(id);

            if (solucao == null)
            {
                throw new Exception("Solução não encontrada");
            }

            solucao.Ativo = false;
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var solucao = await FindByIdAsync(id);

            if (solucao == null)
            {
                throw new Exception("Solução não encontrada");
            }

            solucao.Ativo = true;
            await _context.SaveChangesAsync();
        }
    }
}
