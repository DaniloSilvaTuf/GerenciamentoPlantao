using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class CategoriaService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public CategoriaService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaAcionamento>> FindAllAsync()
        {
            return await _context.CategoriasAcionamento.Include(c => c.Departamento).ToListAsync();
        }

        public async Task<CategoriaAcionamento?> FindByIdAsync(int id)
        {
            return await _context.CategoriasAcionamento.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CriarCategoriaAsync(int departamentoId, string nome)
        {
            var departamento = await _context.Departamentos
                    .Include(d => d.CategoriasAcionamentos)
                    .FirstOrDefaultAsync(d => d.Id == departamentoId);

            if (departamento == null)
                throw new Exception("Departamento não encontrado.");

            var categoria = new CategoriaAcionamento(nome, departamentoId);
            categoria.Ativar();
            departamento.AddCategoria(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditarCategoriaViewModel vm)
        {
            var categoria = await FindByIdAsync(vm.Id);


            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            categoria.Nome = vm.Nome;
            categoria.DepartamentoId = vm.DepartamentoId;
            _context.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var categoria = await FindByIdAsync(id);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            categoria.Ativo = false;
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var categoria = await FindByIdAsync(id);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            categoria.Ativo = true;
            await _context.SaveChangesAsync();
        }
    }
}
