using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Escala;
using GerenciamentoPlantao.Models.Paginacao;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services.Escalas
{
    public class RegraRotacaoEscalaService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public RegraRotacaoEscalaService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<PagedResult<RegraRotacaoEscala>> FindAllAsync(int paginaAtual, int tamanhoPagina, string? busca, string? ordenarPor, string? direcao, int? departamentoId)
        {
            IQueryable<RegraRotacaoEscala> query = _context.RegrasRotacao
                .Include(r => r.Departamento);

            if (!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(p => p.Nome.Contains(busca));
            }

            if (departamentoId.HasValue)
            {
                query = query.Where(p => p.DepartamentoId == departamentoId.Value);
            }


            switch (ordenarPor)
            {
                case "nome":
                    query = direcao == "desc"
                        ? query.OrderByDescending(p => p.Nome)
                        : query.OrderBy(p => p.Nome);
                    break;
                case "departamento":
                    query = direcao == "desc"
                        ? query.OrderByDescending(p => p.Departamento.Nome)
                        : query.OrderBy(p => p.Departamento.Nome);
                    break;
                case "ativo":
                    query = direcao == "desc"
                        ? query.OrderByDescending(p => p.Ativo)
                        : query.OrderBy(p => p.Ativo);
                    break;
                default:
                    query = query.OrderBy(p => p.Id);
                    break;
            }
            return await PagedResult<RegraRotacaoEscala>.CriarAsync(query, paginaAtual, tamanhoPagina);
        }

        public async Task<RegraRotacaoEscala> FindByIdAsync(int id, int departamentoId)
        {
            var regraRotacao = await _context.RegrasRotacao
                .Include(r => r.Departamento)
                .FirstOrDefaultAsync(r => r.Id == id && r.DepartamentoId == departamentoId);

            if (regraRotacao == null)
            {
                throw new NotFoundException("Nenhuma regra encontrada.");
            }

            return regraRotacao;
        }

        public async Task<RegraRotacaoEscala> FindByIdWithAuditAsync(int id, int departamentoId)
        {
            var regraRotacao = await _context.RegrasRotacao
                .Include(r => r.Departamento)
                .Include(r => r.UsuarioInsert)
                .Include(r => r.UsuarioUpdate)
                .FirstOrDefaultAsync(r => r.Id == id && r.DepartamentoId == departamentoId);

            if (regraRotacao == null)
            {
                throw new NotFoundException("Nenhuma regra encontrada.");
            }

            return regraRotacao;
        }

        public async Task InserirRegraRotacaoAsync(RegraRotacaoEscalaViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var departamento = await _context.Departamentos.FirstOrDefaultAsync(d => d.Id == vm.DepartamentoId);

            if (departamento == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            var validacao = await _context.RegrasRotacao
                .AnyAsync(r => r.DepartamentoId == vm.DepartamentoId && r.OrdemExecucao == vm.OrdemExecucao);

            if (validacao)
            {
                throw new BusinessException("Já existe uma regra de rotação com essa ordem de execução para o departamento informado.");
            }

            var regra = new RegraRotacaoEscala
            (
                vm.Nome,
                vm.OrdemExecucao,
                vm.DepartamentoId,
                usuario.Id
            );

            departamento.AddRegraRotacaoEscala(regra);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarRegraRotacaoAsync(EditarRegraRotacaoEscalaViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var regra = await FindByIdAsync(vm.Id, vm.DepartamentoId);

            var validacao = await _context.RegrasRotacao
                .AnyAsync(r => r.DepartamentoId == vm.DepartamentoId && r.OrdemExecucao == vm.OrdemExecucao && r.Id != vm.Id);

            if (validacao)
            {
                throw new BusinessException("Já existe uma regra de rotação com essa ordem de execução para o departamento informado.");
            }
            regra.Atualizar(
                vm.Nome,
                vm.OrdemExecucao,
                usuario.Id);

            await _context.SaveChangesAsync();
        }

        public async Task ExcluirRegraRotacaoAsync(int id, int departamentoId)
        {
            var regra = await FindByIdAsync(id, departamentoId);
            _context.RegrasRotacao.Remove(regra);
            await _context.SaveChangesAsync();
        }
    }
}
