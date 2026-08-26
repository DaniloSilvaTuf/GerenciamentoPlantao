using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class SolucaoService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public SolucaoService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<Solucao>> FindAllAsync()
        {
            return await _context.Solucoes
                .Include(s => s.Departamento)
                .ToListAsync();
        }

        public async Task<List<Solucao>> FindAllActiveAsync()
        {
            return await _context.Solucoes
                .Include(s => s.Departamento)
                .Where(s => s.Ativo)
                .ToListAsync();
        }

        public async Task<List<Solucao>> FindAllDepartmentActiveAsync(int departamentoId)
        {
            return await _context.Solucoes
                .Include(s => s.Departamento)
                .Where(s => s.Ativo && s.DepartamentoId == departamentoId)
                .ToListAsync();
        }

        public async Task<Solucao> FindByIdAsync(int id)
        {
            var solucao = await _context.Solucoes
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (solucao == null)
            {
                throw new NotFoundException("Solução não encontrada.");
            }

            return solucao;
        }

        public async Task<Solucao> FindByIdWithAuditAsync(int id)
        {
            var solucao = await _context.Solucoes
                .Include(s => s.UsuarioInsert)
                .Include(s => s.UsuarioUpdate)
                .Include(s => s.UsuarioInativacao)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solucao == null)
            {
                throw new NotFoundException("Solução não encontrada.");
            }
            return solucao;
        }

        public async Task InserirSolucaoAsync(SolucaoFormViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var departamento = await _context.Departamentos
                .Include(d => d.Solucoes)
                .FirstOrDefaultAsync(d => d.Id == vm.DepartamentoId);

            if (departamento == null) 
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            var solucao = new Solucao
                (
                    vm.Nome, 
                    vm.DepartamentoId,
                    usuario.Id
                );

            departamento.AddSolucao(solucao);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarSolucaoAsync(EditarSolucaoViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var solucao = await FindByIdAsync(vm.Id);
            
            var existe = await _context.Solucoes.AnyAsync(s => 
                s.Id != vm.Id &&
                s.DepartamentoId == vm.DepartamentoId &&
                s.Nome == vm.Nome);

            if (existe)
            {
                throw new BusinessException("Já existe uma solução com esse nome.");
            }

            solucao.Atualizar
                (
                    vm.Nome,
                    vm.DepartamentoId,
                    usuario.Id
                );
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var solucao = await FindByIdAsync(id);

            solucao.Inativar(usuario.Id);
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var solucao = await FindByIdAsync(id);

            solucao.Ativar(usuario.Id);
            await _context.SaveChangesAsync();
        }
    }
}
