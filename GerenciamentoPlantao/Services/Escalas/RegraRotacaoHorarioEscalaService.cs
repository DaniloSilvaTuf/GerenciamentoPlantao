using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Escala;
using GerenciamentoPlantao.Models.Paginacao;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services.Escalas
{
    public class RegraRotacaoHorarioEscalaService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;
        private readonly RegraRotacaoEscalaService _regraRotacaoEscalaService;

        public RegraRotacaoHorarioEscalaService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogadoService, RegraRotacaoEscalaService regraRotacaoEscalaService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
            _regraRotacaoEscalaService = regraRotacaoEscalaService;
        }

        public async Task<List<RegraRotacaoHorarioEscala>> FindAllAsync(int regraRotacaoEscalaId)
        {
            var regraRotacaoHorarioEscala = await _context.RegrasRotacaoHorarios
                .Include(r => r.RegraRotacao)
                .Include(r => r.BlocoHorarioPostoEscala)
                .Where(r => r.RegraRotacaoId == regraRotacaoEscalaId)
                .OrderBy(r => r.OrdemRotacao)
                .ToListAsync();

            return regraRotacaoHorarioEscala;
        }

        public async Task<RegraRotacaoHorarioEscala> FindByIdAsync(int id, int regraRotacaoEscalaId)
        {
            var regraRotacaoHorarioEscala = await _context.RegrasRotacaoHorarios
                .Include(r => r.RegraRotacao)
                .Include(r => r.BlocoHorarioPostoEscala)
                .Where(r => r.Id == id && r.RegraRotacaoId == regraRotacaoEscalaId)
                .FirstOrDefaultAsync();

            if (regraRotacaoHorarioEscala == null)
            {
                throw new NotFoundException("Horário escala não encontrado.");

            }

            return regraRotacaoHorarioEscala;
        }

        public async Task CriarRegraRotacaoHorarioEscalaAsync(RegraRotacaoHorarioEscalaViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            var regraRotacao = await _context.RegrasRotacao
                .FirstOrDefaultAsync(r => r.Id == vm.RegraRotacaoId);

            if (regraRotacao == null)
            {
                throw new NotFoundException("Regra de rotação não encontrada.");
            }

            if (await _context.RegrasRotacaoHorarios.AnyAsync(r => r.RegraRotacaoId == vm.RegraRotacaoId && r.OrdemRotacao == vm.OrdemRotacao))
            {
                throw new BusinessException("A ordem de execução já está em uso para esta regra de rotação.");
            }

            if (await _context.RegrasRotacaoHorarios.AnyAsync(r => r.RegraRotacaoId == vm.RegraRotacaoId && r.BlocoHorarioPostoEscalaId == vm.BlocoHorarioPostoEscalaId))
            {
                throw new BusinessException("O bloco de horário já está em uso para esta regra de rotação.");
            }
            if (!await _context.BlocosHorarioPostosEscala.AnyAsync(b => b.Id == vm.BlocoHorarioPostoEscalaId))
            {
                throw new BusinessException("O bloco de horário não está disponível.");
            }

            var regraRotacaoHorarioEscala = new RegraRotacaoHorarioEscala
            (
                vm.OrdemRotacao,
                vm.RegraRotacaoId,
                vm.BlocoHorarioPostoEscalaId,
                usuario.Id
            );

            regraRotacao.AddRegraRotacaoHorarioEscala(regraRotacaoHorarioEscala);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarRegraRotacaoHorarioEscalaAsync(EditarRegraRotacaoHorarioEscalaViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();

            var regraRotacaoHorarioEscala = await _context.RegrasRotacaoHorarios
                .Include(r => r.RegraRotacao)
                .FirstOrDefaultAsync(r => r.Id == vm.Id && r.RegraRotacaoId == vm.RegraRotacaoId);

            if (regraRotacaoHorarioEscala == null)
            {
                throw new NotFoundException("Horário escala não encontrado.");
            }

            if (await _context.RegrasRotacaoHorarios.AnyAsync(r => r.RegraRotacaoId == vm.RegraRotacaoId && r.OrdemRotacao == vm.OrdemRotacao && r.Id != vm.Id))
            {
                throw new BusinessException("A ordem de execução já está em uso para esta regra de rotação.");
            }

            if (await _context.RegrasRotacaoHorarios.AnyAsync(r => r.RegraRotacaoId == vm.RegraRotacaoId && r.BlocoHorarioPostoEscalaId == vm.BlocoHorarioPostoEscalaId && r.Id != vm.Id))
            {
                throw new BusinessException("O bloco de horário já está em uso para esta regra de rotação.");
            }
            if (!await _context.BlocosHorarioPostosEscala.AnyAsync(b => b.Id == vm.BlocoHorarioPostoEscalaId))
            {
                throw new BusinessException("O bloco de horário não está disponível.");
            }

            regraRotacaoHorarioEscala.AtualizarRegraRotacaoHorarioEscala
                (
                    vm.OrdemRotacao,
                    vm.BlocoHorarioPostoEscalaId,
                    usuario.Id
                );

            await _context.SaveChangesAsync();
        }

        public async Task ExcluirRegraRotacaoHorarioEscalaAsync(int id, int regraRotacaoEscalaId)
        {
            var regraRotacaoHorarioEscala = await _context.RegrasRotacaoHorarios
                .Include(r => r.RegraRotacao)
                .FirstOrDefaultAsync(r => r.Id == id && r.RegraRotacaoId == regraRotacaoEscalaId);

            if (regraRotacaoHorarioEscala == null)
            {
                throw new NotFoundException("Horário escala não encontrado.");
            }

            _context.RegrasRotacaoHorarios.Remove(regraRotacaoHorarioEscala);
            await _context.SaveChangesAsync();
        }
    }
}
