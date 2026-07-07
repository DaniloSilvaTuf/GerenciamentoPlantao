using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class DepartamentoService
    {

        private readonly GerenciamentoPlantaoContext _context;

        public DepartamentoService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamentos.OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<List<Departamento>> FindAllActiveAsync()
        {
            return await _context.Departamentos.Where(e => e.Ativo).OrderBy(e => e.Nome).ToListAsync();
        }

        public async Task<Departamento?> FindByIdAsync(int id)
        {
            return await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Departamento departamento)
        {
            departamento.Ativar();
            _context.Add(departamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Departamento departamento)
        {
            _context.Update(departamento);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento == null)
            {
                throw new Exception("Departamento não encontrado.");
            }

            departamento.Inativar();
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento == null)
            {
                throw new Exception("Departamento não encontrado.");
            }

            departamento.Ativar();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Departamentos.AnyAsync(x => x.Id == id);
        }
    }
}
