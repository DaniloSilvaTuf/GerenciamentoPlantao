
using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Enums;
using GerenciamentoPlantao.Models.Escala;
using GerenciamentoPlantao.Models.Paginacao;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services.Escalas
{
    public class BlocoHorarioEscalaService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogado;

        public BlocoHorarioEscalaService(
            GerenciamentoPlantaoContext context,
            IUsuarioLogadoService usuarioLogado)
        {
            _context = context;
            _usuarioLogado = usuarioLogado;
        }

        // =====================================================
        // LISTAGEM
        // =====================================================
        public async Task<PagedResult<BlocoHorarioEscala>> FindAllAsync(
            int paginaAtual,
            int tamanhoPagina,
            string? busca,
            string? ordenarPor,
            string? direcao,
            int? departamentoId,
            TipoDiaSemana? diaSemana)
        {
            IQueryable<BlocoHorarioEscala> query =
                _context.BlocosHorarioEscala
                    .AsNoTracking()
                    .Include(b => b.PostoEscala)
                        .ThenInclude(p => p.Departamento);

            if (!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(b =>
                    b.Nome.Contains(busca));
            }

            if (departamentoId.HasValue)
            {
                query = query.Where(b =>
                    b.PostoEscala.DepartamentoId ==
                    departamentoId.Value);
            }

            if (diaSemana.HasValue)
            {
                query = query.Where(b =>
                    b.DiaSemana == diaSemana.Value);
            }

            switch (ordenarPor)
            {
                case "nome":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.Nome)
                        : query.OrderBy(b => b.Nome);
                    break;

                case "departamento":
                    query = direcao == "desc"
                        ? query.OrderByDescending(
                            b => b.PostoEscala.Departamento.Nome)
                        : query.OrderBy(
                            b => b.PostoEscala.Departamento.Nome);
                    break;

                case "posto":
                    query = direcao == "desc"
                        ? query.OrderByDescending(
                            b => b.PostoEscala.Nome)
                        : query.OrderBy(
                            b => b.PostoEscala.Nome);
                    break;

                case "dia":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.DiaSemana)
                        : query.OrderBy(b => b.DiaSemana);
                    break;

                case "ativo":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.Ativo)
                        : query.OrderBy(b => b.Ativo);
                    break;

                default:
                    query = query
                        .OrderBy(b =>
                            b.PostoEscala.Departamento.Nome)
                        .ThenBy(b => b.PostoEscala.Nome)
                        .ThenBy(b => b.DiaSemana)
                        .ThenBy(b => b.HoraInicio);
                    break;
            }

            return await PagedResult<BlocoHorarioEscala>
                .CriarAsync(
                    query,
                    paginaAtual,
                    tamanhoPagina);
        }

        // =====================================================
        // BUSCAR POR ID
        // =====================================================
        public async Task<BlocoHorarioEscala> FindByIdAsync(
            int id,
            int departamentoId)
        {
            var bloco = await _context.BlocosHorarioEscala
                .Include(b => b.PostoEscala)
                    .ThenInclude(p => p.Departamento)
                .FirstOrDefaultAsync(b =>
                    b.Id == id &&
                    b.PostoEscala.DepartamentoId ==
                    departamentoId);

            if (bloco == null)
            {
                throw new NotFoundException(
                    "Bloco de horário não encontrado.");
            }

            return bloco;
        }

        // =====================================================
        // BUSCAR POR ID COM AUDITORIA
        // =====================================================
        public async Task<BlocoHorarioEscala> FindByIdWithAuditAsync(
            int id,
            int departamentoId)
        {
            var bloco = await _context.BlocosHorarioEscala
                .AsNoTracking()
                .Include(b => b.PostoEscala)
                    .ThenInclude(p => p.Departamento)
                .Include(b => b.UsuarioInsert)
                .Include(b => b.UsuarioUpdate)
                .Include(b => b.UsuarioInativacao)
                .FirstOrDefaultAsync(b =>
                    b.Id == id &&
                    b.PostoEscala.DepartamentoId ==
                    departamentoId);

            if (bloco == null)
            {
                throw new NotFoundException(
                    "Bloco de horário não encontrado.");
            }

            return bloco;
        }

        // =====================================================
        // BUSCAR HORÁRIOS POR POSTO
        // =====================================================
        public async Task<List<BlocoHorarioEscala>> FindByPostoAsync(
            int postoEscalaId)
        {
            return await _context.BlocosHorarioEscala
                .AsNoTracking()
                .Where(b =>
                    b.PostoEscalaId == postoEscalaId)
                .OrderBy(b => b.DiaSemana)
                .ThenBy(b => b.HoraInicio)
                .ToListAsync();
        }

        // =====================================================
        // INSERIR BLOCO DE HORÁRIO
        // =====================================================
        public async Task InserirBlocoHorarioAsync(
            BlocoHorarioEscalaViewModel vm)
        {
            var usuario = await _usuarioLogado
                .ObterUsuarioLogadoAsync();

            var posto = await _context.PostosEscalas
                .FirstOrDefaultAsync(p =>
                    p.Id == vm.PostoEscalaId);

            if (posto == null)
            {
                throw new NotFoundException(
                    "Posto de escala não encontrado.");
            }

            if (string.IsNullOrWhiteSpace(vm.Nome))
            {
                throw new BusinessException(
                    "Informe o nome do bloco de horário.");
            }

            var nome = vm.Nome.Trim();

            if (nome.Length < 3 || nome.Length > 150)
            {
                throw new BusinessException(
                    "O nome do horário deve ter entre " +
                    "3 e 150 caracteres.");
            }

            if (vm.DiasSemana == null ||
                !vm.DiasSemana.Any())
            {
                throw new BusinessException(
                    "Selecione pelo menos um dia da semana.");
            }

            var dias = vm.DiasSemana
                .Distinct()
                .ToList();

            if (dias.Any(d =>
                !Enum.IsDefined(typeof(TipoDiaSemana), d)))
            {
                throw new BusinessException(
                    "Um dos dias da semana selecionados é inválido.");
            }

            var diasExistentes =
                await _context.BlocosHorarioEscala
                    .Where(b =>
                        b.PostoEscalaId == vm.PostoEscalaId &&
                        b.Nome == nome &&
                        dias.Contains(b.DiaSemana))
                    .Select(b => b.DiaSemana)
                    .ToListAsync();

            if (diasExistentes.Any())
            {
                throw new BusinessException(
                    "Já existe um bloco com esse nome em " +
                    "um dos dias selecionados para este posto.");
            }

            foreach (var dia in dias)
            {
                var bloco = new BlocoHorarioEscala(
                    nome,
                    vm.HoraInicio,
                    vm.HoraFim,
                    dia,
                    vm.PostoEscalaId,
                    usuario.Id
                );

                posto.AddBlocoHorarioEscala(bloco);
            }

            await _context.SaveChangesAsync();
        }

        // =====================================================
        // ATUALIZAR BLOCO DE HORÁRIO
        // =====================================================
        public async Task AtualizarBlocoHorarioAsync(
            EditarBlocoHorarioEscalaViewModel vm)
        {
            var usuario = await _usuarioLogado
                .ObterUsuarioLogadoAsync();

            var bloco = await FindByIdAsync(
                vm.Id,
                vm.DepartamentoId);

            // Validação do nome.
            if (string.IsNullOrWhiteSpace(vm.Nome))
            {
                throw new BusinessException(
                    "Informe o nome do bloco de horário.");
            }

            var nome = vm.Nome.Trim();

            if (nome.Length < 3 || nome.Length > 150)
            {
                throw new BusinessException(
                    "O nome do horário deve ter entre " +
                    "3 e 150 caracteres.");
            }

            // Validação do dia.
            if (!vm.DiaSemana.HasValue ||
                !Enum.IsDefined(
                    typeof(TipoDiaSemana),
                    vm.DiaSemana.Value))
            {
                throw new BusinessException(
                    "Selecione um dia da semana válido.");
            }

            // Verifica duplicidade dentro do posto.
            var nomeExiste = await _context.BlocosHorarioEscala
                .AnyAsync(b =>
                    b.Nome == nome &&
                    b.PostoEscalaId == bloco.PostoEscalaId &&
                    b.DiaSemana == vm.DiaSemana.Value &&
                    b.Id != vm.Id);

            if (nomeExiste)
            {
                throw new BusinessException(
                    "Já existe um bloco com esse nome " +
                    "neste posto e dia da semana.");
            }

            // Identifica se houve alteração em campos protegidos.
            var alterouDia =
                bloco.DiaSemana != vm.DiaSemana.Value;

            var alterouInicio =
                bloco.HoraInicio != vm.HoraInicio;

            var alterouFim =
                bloco.HoraFim != vm.HoraFim;

            // Nome pode ser alterado mesmo quando o bloco
            // estiver vinculado.
            //
            // Dia, hora inicial e hora final só podem
            // ser alterados quando não houver vínculos.
            if (alterouDia || alterouInicio || alterouFim)
            {
                var possuiVinculo =
                    await PossuiVinculosAsync(bloco.Id);

                if (possuiVinculo)
                {
                    throw new BusinessException(
                        $"O horário '{bloco.Nome}' do dia " +
                        $"{bloco.DiaSemana} está vinculado " +
                        "a uma regra de rotação ou escala-base. " +
                        "Somente o nome pode ser alterado.");
                }
            }

            // Atualiza somente após passar por todas as validações.
            bloco.Atualizar(
                nome,
                vm.HoraInicio,
                vm.HoraFim,
                vm.DiaSemana.Value,
                usuario.Id
            );

            await _context.SaveChangesAsync();
        }

        // =====================================================
        // ATIVAR BLOCO DE HORÁRIO
        // =====================================================
        public async Task AtivarBlocoHorarioEscalaAsync(
            int id,
            int departamentoId)
        {
            var usuario = await _usuarioLogado
                .ObterUsuarioLogadoAsync();

            var bloco = await FindByIdAsync(
                id,
                departamentoId);

            bloco.Ativar(usuario.Id);

            await _context.SaveChangesAsync();
        }

        // =====================================================
        // INATIVAR BLOCO DE HORÁRIO
        // =====================================================
        public async Task InativarBlocoHorarioEscalaAsync(
            int id,
            int departamentoId)
        {
            var usuario = await _usuarioLogado
                .ObterUsuarioLogadoAsync();

            var bloco = await FindByIdAsync(
                id,
                departamentoId);

            // Não permite inativar horários vinculados
            // a regras de rotação ou escalas-base.
            var possuiVinculo =
                await PossuiVinculosAsync(bloco.Id);

            if (possuiVinculo)
            {
                throw new BusinessException(
                    $"O horário '{bloco.Nome}' não pode ser " +
                    "inativado, pois está vinculado a uma " +
                    "regra de rotação ou escala-base.");
            }

            bloco.Inativar(usuario.Id);

            await _context.SaveChangesAsync();
        }

        // =====================================================
        // EXCLUIR BLOCO DE HORÁRIO
        // =====================================================
        public async Task ExcluirBlocoHorarioAsync(
            int id,
            int departamentoId)
        {
            var bloco = await FindByIdAsync(
                id,
                departamentoId);

            // Não permite excluir horários vinculados.
            var possuiVinculo =
                await PossuiVinculosAsync(bloco.Id);

            if (possuiVinculo)
            {
                throw new BusinessException(
                    $"O horário '{bloco.Nome}' não pode ser " +
                    "excluído, pois está vinculado a uma " +
                    "regra de rotação ou escala-base.");
            }

            _context.BlocosHorarioEscala.Remove(bloco);

            await _context.SaveChangesAsync();
        }

        // =====================================================
        // MÉTODOS PRIVADOS
        // =====================================================

        /// <summary>
        /// Verifica se um bloco de horário está associado
        /// a alguma regra de rotação ou item de escala-base.
        /// </summary>
        private async Task<bool> PossuiVinculosAsync(int blocoId)
        {
            var utilizadoEmRotacao =
                await _context.RegrasRotacaoHorarios
                    .AnyAsync(r =>
                        r.BlocoHorarioEscalaId == blocoId);

            if (utilizadoEmRotacao)
            {
                return true;
            }

            var utilizadoEmEscalaBase =
                await _context.EscalasBaseItems
                    .AnyAsync(e =>
                        e.BlocoHorarioEscalaId == blocoId);

            return utilizadoEmEscalaBase;
        }
    }
}
