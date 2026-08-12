using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class DepartamentoService
    {

        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public DepartamentoService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamentos
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<List<Departamento>> FindAllActiveAsync()
        {
            return await _context.Departamentos.Where(e => e.Ativo)
                .OrderBy(e => e.Nome)
                .ToListAsync();
        }

        public async Task<Departamento> FindByIdAsync(int id)
        {
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (departamento == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            return departamento;
        }

        public async Task<Departamento> FindByIdWithAuditAsync(int id)
        {
            var departamento = await _context.Departamentos
                .Include(d => d.UsuarioInsert)
                .Include(d => d.UsuarioUpdate)
                .Include(d => d.UsuarioInativacao)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (departamento == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }
            return departamento;
        }

        public async Task InserirDepartamentoAsync(DepartamentoFormViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var existe = await _context.Departamentos.AnyAsync(x => x.Nome == vm.Nome);

            if (existe)
            {
                throw new BusinessException("Já existe um departamento com o mesmo nome.");
            }

            var departamento = new Departamento
                (
                    vm.Nome,
                    usuario.Id
                );

            _context.Add(departamento);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarDepartamentoAsync(EditarDepartamentoViewModel vm)
        {
            var departamento = await FindByIdAsync(vm.Id);
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            var existe = await _context.Departamentos.AnyAsync(x =>
                x.Id != vm.Id &&
                x.Nome == vm.Nome);

            if (existe) 
            {
                throw new BusinessException("Já existe um departamento com o mesmo nome.");
            }

            departamento.Atualizar
                (
                    vm.Nome,
                    usuario.Id
                );

            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var departamento = await FindByIdAsync(id);
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            departamento.Inativar(usuario.Id);
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var departamento = await FindByIdAsync(id);
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            departamento.Ativar(usuario.Id);
            await _context.SaveChangesAsync();
        }
    }
}
