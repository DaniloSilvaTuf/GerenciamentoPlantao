using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class EstabelecimentoService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public EstabelecimentoService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<Estabelecimento>> FindAllAsync()
        {
            return await _context.Estabelecimentos
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<List<Estabelecimento>>FindAllActiveAsync()
        {
            return await _context.Estabelecimentos
                .Where(e => e.Ativo)
                .OrderBy(e => e.Nome)
                .ToListAsync();
        }

        public async Task<Estabelecimento> FindByIdAsync(int id)
        {
            var estabelecimento = await _context.Estabelecimentos.FirstOrDefaultAsync(x => x.Id == id);
            if (estabelecimento == null)
            {
                throw new NotFoundException("Estabelecimento não encontrado.");
            }
            return estabelecimento;
        }

        public async Task<Estabelecimento> FindByIdWithAuditAsync(int id)
        {
            var estabelecimento = await _context.Estabelecimentos
                .Include(e => e.UsuarioInsert)
                .Include(e => e.UsuarioUpdate)
                .Include(e => e.UsuarioInativacao)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estabelecimento == null)
            {
                throw new NotFoundException("Estabelecimento não encontrado.");
            }
            return estabelecimento;
        }

        public async Task InserirEstabelecimentoAsync(EstabelecimentoFormViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var existe = await _context.Estabelecimentos.AnyAsync(e => e.Nome == vm.Nome);

            if (existe)
            {
                throw new BusinessException("Já existe um estabelecimento com o mesmo nome.");
            }

            var estabelecimento = new Estabelecimento
                (
                    vm.Nome,
                    usuario.Id
                );

            _context.Add(estabelecimento);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarEstabelecimentoAsync(EditarEstabelecimentoViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var estabelecimento = await FindByIdAsync(vm.Id);

            var existe = await _context.Estabelecimentos.AnyAsync(e =>
                e.Id != vm.Id &&
                e.Nome == vm.Nome);

            if (existe)
            {
                throw new BusinessException("Já existe um estabelecimento com o mesmo nome.");
            }

            estabelecimento.Atualizar
                (
                    vm.Nome,
                    usuario.Id
                );

            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var estabelecimento = await FindByIdAsync(id);
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            estabelecimento.Inativar(usuario.Id);
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var estabelecimento = await FindByIdAsync(id);
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            estabelecimento.Ativar(usuario.Id);
            await _context.SaveChangesAsync();
        }
    }
}