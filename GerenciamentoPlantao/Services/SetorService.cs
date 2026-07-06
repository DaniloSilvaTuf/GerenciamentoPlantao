using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;
using GerenciamentoPlantao.Controllers;
using GerenciamentoPlantao.Models.ViewModels;

namespace GerenciamentoPlantao.Services
{
    public class SetorService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public SetorService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }

        public async Task<List<Setor>> FindAllAsync()
        {
            return await _context.Setores.Include(s => s.Estabelecimento).ToListAsync();
        }

        public async Task<Setor?> FindByIdAsync(int id)
        {
            return await _context.Setores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CriarSetorAsync(int estabelecimentoId, string nome)
        {
            var estabelecimento = await _context.Estabelecimentos
                    .Include(e => e.Setores)
                    .FirstOrDefaultAsync(e => e.Id == estabelecimentoId);

            if (estabelecimento == null)
                throw new Exception("Estabelecimento não encontrado.");

            var setor = new Setor(nome, estabelecimentoId);
            setor.Ativar();
            estabelecimento.AddSetor(setor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditarSetorViewModel vm)
        {
            var setor = await FindByIdAsync(vm.Id);
 
        
        if(setor == null)
            {
                throw new Exception("Setor não encontrado");
            }

            setor.Nome = vm.Nome;
            setor.EstabelecimentoId = vm.EstabelecimentoId;
            _context.Update(setor);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var setor = await FindByIdAsync(id);

            if (setor == null)
            {
                throw new Exception("Setor não encontrado.");
            }

            setor.Ativo = false;
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var setor = await FindByIdAsync(id);

            if (setor == null)
            {
                throw new Exception("Setor não encontrado.");
            }

            setor.Ativo = true;
            await _context.SaveChangesAsync();
        }
    }
}