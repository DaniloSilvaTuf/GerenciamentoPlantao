using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class CategoriaService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public CategoriaService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<CategoriaAcionamento>> FindAllAsync()
        {
            return await _context.CategoriasAcionamento
                .Include(c => c.Departamento)
                .ToListAsync();
        }

        public async Task<List<CategoriaAcionamento>> FindAllActiveAsync()
        {
            return await _context.CategoriasAcionamento
                .Include(c => c.Departamento)
                .Where(c => c.Ativo)
                .ToListAsync();
        }

        public async Task<CategoriaAcionamento> FindByIdAsync(int id)
        {
            var categoria = await _context.CategoriasAcionamento
                .FirstOrDefaultAsync(x => x.Id == id);

            if (categoria == null)
            {
                throw new NotFoundException("Categoria não encontrada.");
            }

            return categoria;
        }

        public async Task<CategoriaAcionamento> FindByIdWithAuditAsync(int id)
        {
            var categoria = await _context.CategoriasAcionamento
                .Include(c => c.UsuarioInsert)
                .Include(c => c.UsuarioUpdate)
                .Include(c => c.UsuarioInativacao)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                throw new NotFoundException("Categoria não encontrada.");
            }
            return categoria;
        }

        public async Task CriarCategoriaAsync(CategoriaFormViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            var departamento = await _context.Departamentos
                    .Include(d => d.CategoriasAcionamentos)
                    .FirstOrDefaultAsync(d => d.Id == vm.DepartamentoId);

            if (departamento == null)
                throw new NotFoundException("Departamento não encontrado.");

            var categoria = new CategoriaAcionamento
            (
                vm.Nome,
                vm.DepartamentoId,
                usuario.Id
            );
            departamento.AddCategoria(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarCategoriaAsync(EditarCategoriaViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var categoria = await FindByIdAsync(vm.Id);

            var existe = await _context.CategoriasAcionamento.AnyAsync(c =>
                c.Id != vm.Id &&
                c.DepartamentoId == vm.DepartamentoId &&
                c.Nome == vm.Nome);

            if (existe)
            {
                throw new BusinessException("Já existe uma categoria com o mesmo nome neste departamento.");
            }

            categoria.Atualizar
            (
                vm.Nome,
                vm.DepartamentoId,
                usuario.Id
            );
            await _context.SaveChangesAsync();
        }

        public async Task InativarCategoriaAsync(int id)
        {
            var categoria = await FindByIdAsync(id);
            categoria.Ativo = false;
            await _context.SaveChangesAsync();
        }

        public async Task AtivarCategoriaAsync(int id)
        {
            var categoria = await FindByIdAsync(id);
            categoria.Ativo = true;
            await _context.SaveChangesAsync();
        }
    }
}
