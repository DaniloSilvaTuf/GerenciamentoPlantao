using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class EstabelecimentoService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public EstabelecimentoService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }

        public async Task<List<Estabelecimento>> FindAllAsync()
        {
            return await _context.Estabelecimentos.OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<List<Estabelecimento>>FindAllActiveAsync()
        {
            return await _context.Estabelecimentos.Where(e => e.Ativo).OrderBy(e => e.Nome).ToListAsync();
        }

        public async Task<Estabelecimento?> FindByIdAsync(int id)
        {
            return await _context.Estabelecimentos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Estabelecimento estabelecimento)
        {
            estabelecimento.Ativar();
            _context.Add(estabelecimento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Estabelecimento estabelecimento)
        {
            _context.Update(estabelecimento);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var estabelecimento = await _context.Estabelecimentos.FindAsync(id);
            
            if(estabelecimento == null)
            {
                throw new Exception("Estabelecimento não encontrado.");
            }

            estabelecimento.Inativar();
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var estabelecimento = await _context.Estabelecimentos.FindAsync(id);

            if(estabelecimento == null)
            {
                throw new Exception("Estabelecimento não encontrado.");
            }

            estabelecimento.Ativar();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Estabelecimentos.AnyAsync(x => x.Id == id);
        }
    }
}