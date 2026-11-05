using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Escala;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services.Escalas
{
    public class RegraRotacaoHorarioEscalaService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public RegraRotacaoHorarioEscalaService(
            GerenciamentoPlantaoContext context,
            IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<RegraRotacaoHorarioEscala>> FindAllAsync(
            int regraRotacaoEscalaId)
        {
            return await _context.RegrasRotacaoHorarios
                .AsNoTracking()
                .Include(r => r.RegraRotacao)
                .Include(r => r.BlocoHorarioEscala)
                    .ThenInclude(b => b.PostoEscala)
                .Where(r => r.RegraRotacaoId == regraRotacaoEscalaId)
                .OrderBy(r => r.OrdemRotacao)
                .ToListAsync();
        }

        public async Task<RegraRotacaoHorarioEscala> FindByIdAsync(
            int id,
            int regraRotacaoEscalaId)
        {
            var horario = await _context.RegrasRotacaoHorarios
                .AsNoTracking()
                .Include(r => r.RegraRotacao)
                .Include(r => r.BlocoHorarioEscala)
                    .ThenInclude(b => b.PostoEscala)
                .FirstOrDefaultAsync(r =>
                    r.Id == id &&
                    r.RegraRotacaoId == regraRotacaoEscalaId);

            if (horario == null)
            {
                throw new NotFoundException(
                    "Horário da regra de rotação não encontrado.");
            }

            return horario;
        }

        public async Task CriarRegraRotacaoHorarioEscalaAsync(
            RegraRotacaoHorarioEscalaViewModel vm)
        {
            if (vm.OrdemRotacao <= 0)
            {
                throw new BusinessException(
                    "A ordem de rotação deve ser maior que zero.");
            }

            var usuario = await _usuarioLogadoService
                .ObterUsuarioLogadoAsync();

            var regra = await _context.RegrasRotacao
                .FirstOrDefaultAsync(r =>
                    r.Id == vm.RegraRotacaoId);

            if (regra == null)
            {
                throw new NotFoundException(
                    "Regra de rotação não encontrada.");
            }

            // Verifica se o horário existe e pertence
            // a um posto do mesmo departamento da regra.
            var blocoValido = await _context.BlocosHorarioEscala
                .AnyAsync(b =>
                    b.Id == vm.BlocoHorarioEscalaId &&
                    b.PostoEscala.DepartamentoId == regra.DepartamentoId);

            if (!blocoValido)
            {
                throw new BusinessException(
                    "O horário selecionado não existe ou não pertence ao departamento desta regra.");
            }

            // Impede duplicidade na ordem de rotação.
            var ordemExiste = await _context.RegrasRotacaoHorarios
                .AnyAsync(r =>
                    r.RegraRotacaoId == vm.RegraRotacaoId &&
                    r.OrdemRotacao == vm.OrdemRotacao);

            if (ordemExiste)
            {
                throw new BusinessException(
                    "A ordem de rotação já está em uso nesta regra.");
            }

            // Impede que o mesmo horário seja incluído
            // duas vezes na mesma regra.
            var horarioExiste = await _context.RegrasRotacaoHorarios
                .AnyAsync(r =>
                    r.RegraRotacaoId == vm.RegraRotacaoId &&
                    r.BlocoHorarioEscalaId == vm.BlocoHorarioEscalaId);

            if (horarioExiste)
            {
                throw new BusinessException(
                    "Este horário já está cadastrado nesta regra de rotação.");
            }

            var horario = new RegraRotacaoHorarioEscala(
                vm.OrdemRotacao,
                vm.RegraRotacaoId,
                vm.BlocoHorarioEscalaId,
                usuario.Id
            );

            regra.AddRegraRotacaoHorarioEscala(horario);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarRegraRotacaoHorarioEscalaAsync(
            EditarRegraRotacaoHorarioEscalaViewModel vm)
        {
            if (vm.OrdemRotacao <= 0)
            {
                throw new BusinessException(
                    "A ordem de rotação deve ser maior que zero.");
            }

            var usuario = await _usuarioLogadoService
                .ObterUsuarioLogadoAsync();

            var horario = await _context.RegrasRotacaoHorarios
                .Include(r => r.RegraRotacao)
                .FirstOrDefaultAsync(r =>
                    r.Id == vm.Id &&
                    r.RegraRotacaoId == vm.RegraRotacaoId);

            if (horario == null)
            {
                throw new NotFoundException(
                    "Horário da regra de rotação não encontrado.");
            }

            // Valida se o novo bloco pertence
            // ao departamento da regra.
            var blocoValido = await _context.BlocosHorarioEscala
                .AnyAsync(b =>
                    b.Id == vm.BlocoHorarioEscalaId &&
                    b.PostoEscala.DepartamentoId ==
                        horario.RegraRotacao.DepartamentoId);

            if (!blocoValido)
            {
                throw new BusinessException(
                    "O horário selecionado não existe ou não pertence ao departamento desta regra.");
            }

            // Verifica duplicidade de ordem,
            // desconsiderando o próprio registro.
            var ordemExiste = await _context.RegrasRotacaoHorarios
                .AnyAsync(r =>
                    r.RegraRotacaoId == vm.RegraRotacaoId &&
                    r.OrdemRotacao == vm.OrdemRotacao &&
                    r.Id != vm.Id);

            if (ordemExiste)
            {
                throw new BusinessException(
                    "A ordem de rotação já está em uso nesta regra.");
            }

            // Verifica duplicidade de horário,
            // desconsiderando o próprio registro.
            var horarioExiste = await _context.RegrasRotacaoHorarios
                .AnyAsync(r =>
                    r.RegraRotacaoId == vm.RegraRotacaoId &&
                    r.BlocoHorarioEscalaId == vm.BlocoHorarioEscalaId &&
                    r.Id != vm.Id);

            if (horarioExiste)
            {
                throw new BusinessException(
                    "Este horário já está cadastrado nesta regra de rotação.");
            }

            horario.AtualizarRegraRotacaoHorarioEscala(
                vm.OrdemRotacao,
                vm.BlocoHorarioEscalaId,
                usuario.Id
            );

            await _context.SaveChangesAsync();
        }

        public async Task ExcluirRegraRotacaoHorarioEscalaAsync(
            int id,
            int regraRotacaoEscalaId)
        {
            var horario = await _context.RegrasRotacaoHorarios
                .FirstOrDefaultAsync(r =>
                    r.Id == id &&
                    r.RegraRotacaoId == regraRotacaoEscalaId);

            if (horario == null)
            {
                throw new NotFoundException(
                    "Horário da regra de rotação não encontrado.");
            }

            _context.RegrasRotacaoHorarios.Remove(horario);

            await _context.SaveChangesAsync();
        }
    }
}