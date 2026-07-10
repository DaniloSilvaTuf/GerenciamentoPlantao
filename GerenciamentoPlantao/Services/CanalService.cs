using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class CanalService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public CanalService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }

        public async Task<List<Canal>> FindAllAsync()
        {
            return await _context.Canais.Include(c => c.Departamento).ToListAsync();
        }

        public async Task<Canal?> FindByIdAsync(int id)
        {
            return await _context.Canais.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CriarCanalAsync(int departamentoId, string nome)
        {
            var departamento = await _context.Departamentos
                    .Include(d => d.Canais)
                    .FirstOrDefaultAsync(d => d.Id == departamentoId);

            if (departamento == null)
                throw new Exception("Departamento não encontrado.");

            var canal = new Canal(nome, departamentoId);
            canal.Ativar();
            departamento.AddCanal(canal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditarCanalViewModel vm)
        {
            var canal = await FindByIdAsync(vm.Id);


            if (canal == null)
            {
                throw new Exception("Canal não encontrado.");
            }

            canal.Nome = vm.Nome;
            canal.DepartamentoId = vm.DepartamentoId;
            _context.Update(canal);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var canal = await FindByIdAsync(id);

            if (canal == null)
            {
                throw new Exception("Canal não encontrado.");
            }

            canal.Ativo = false;
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var canal = await FindByIdAsync(id);

            if (canal == null)
            {
                throw new Exception("Canal não encontrado.");
            }

            canal.Ativo = true;
            await _context.SaveChangesAsync();
        }
    }
}
