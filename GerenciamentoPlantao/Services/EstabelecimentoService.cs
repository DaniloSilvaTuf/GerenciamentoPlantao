using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Paginacao;
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

        public async Task<PagedResult<Estabelecimento>> FindAllAsync(int paginaAtual, int tamanhoPagina, string? busca, string? ordenarPor, string? direcao)
        {
            var query = _context.Estabelecimentos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(x => x.Nome.Contains(busca));
            }

            switch (ordenarPor)
            {
                case "nome":
                    query = direcao == "desc"
                        ? query.OrderByDescending(x => x.Nome)
                        : query.OrderBy(x => x.Nome);
                    break;

                case "ativo":
                    query = direcao == "desc"
                        ? query.OrderByDescending(x => x.Ativo)
                        : query.OrderBy(x => x.Ativo);
                    break;

                default:
                    query = query.OrderBy(x => x.Nome);
                    break;
            }

            return await PagedResult<Estabelecimento>.CriarAsync(query, paginaAtual, tamanhoPagina);
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