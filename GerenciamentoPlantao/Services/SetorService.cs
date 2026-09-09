using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Paginacao;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class SetorService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public SetorService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<PagedResult<Setor>> FindAllAsync(int paginaAtual, int tamanhoPagina, string? busca, string? ordenarPor, string? direcao, int? estabelecimentoId)
        {
            IQueryable<Setor> query = _context.Setores
                .Include(s => s.Estabelecimento);

            if (!string.IsNullOrEmpty(busca))
            {
                query = query.Where(s => s.Nome.Contains(busca));
            }

            if (estabelecimentoId.HasValue)
            {
                query = query.Where(s => s.EstabelecimentoId == estabelecimentoId.Value);
            }
            
            switch (ordenarPor)
            {
                case "nome":
                    query = direcao == "desc" 
                        ? query.OrderByDescending(s => s.Nome) 
                        : query.OrderBy(s => s.Nome);
                    break;

                case "estabelecimento":
                    query = direcao == "desc" 
                        ? query.OrderByDescending(s => s.Estabelecimento.Nome) 
                        : query.OrderBy(s => s.Estabelecimento.Nome);
                    break;

                default:
                    query = query.OrderBy(s => s.Nome);
                    break;
            }

            return await PagedResult<Setor>.CriarAsync(query, paginaAtual, tamanhoPagina);
        }

        public async Task<List<Setor>> FindAllActiveAsync()
        {
            return await _context.Setores
                .Where(s => s.Ativo)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<List<Setor>> FindAllActiveEstabelecimentoAsync(int estabelecimentoId)
        {
            return await _context.Setores
                .Where(s => s.Ativo && s.EstabelecimentoId == estabelecimentoId)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<Setor> FindByIdAsync(int id)
        {
            var setor = await _context.Setores.FirstOrDefaultAsync(x => x.Id == id);
            
            if (setor == null)
                throw new NotFoundException("Setor não encontrado.");
            return setor;
        }

        public async Task<Setor> FindByIdWithAuditAsync(int id)
        {
            var setor = await _context.Setores
                .Include(s => s.UsuarioInsert)
                .Include(s => s.UsuarioUpdate)
                .Include(s => s.UsuarioInativacao)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (setor == null)
            {
                throw new NotFoundException("Setor não encontrado.");
            }
            return setor;
        }

        public async Task InserirSetorAsync(SetorFormViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var estabelecimento = await _context.Estabelecimentos
                    .Include(e => e.Setores)
                    .FirstOrDefaultAsync(e => e.Id == vm.EstabelecimentoId);

            if (estabelecimento == null)
                throw new NotFoundException("Estabelecimento não encontrado.");
                
            var setor = new Setor
                (
                    vm.Nome,
                    vm.EstabelecimentoId,
                    usuario.Id
                );

            estabelecimento.AddSetor(setor);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarSetorAsync(EditarSetorViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var setor = await FindByIdAsync(vm.Id);

            var existe = await _context.Setores.AnyAsync(s => 
                s.Id != vm.Id &&
                s.EstabelecimentoId == vm.EstabelecimentoId &&
                s.Nome == vm.Nome);

            if (existe)
            {
                throw new BusinessException("Já existe um setor com o mesmo nome neste estabelecimento.");
            }

            setor.AtualizarSetor
                (
                    vm.Nome,
                    vm.EstabelecimentoId,
                    usuario.Id
                );

            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var setor = await FindByIdAsync(id);

            setor.Inativar(usuario.Id);
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var setor = await FindByIdAsync(id);

            setor.Ativar(usuario.Id);
            await _context.SaveChangesAsync();
        }
    }
}