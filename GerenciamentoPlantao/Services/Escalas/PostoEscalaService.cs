using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Enums;
using GerenciamentoPlantao.Models.Escala;
using GerenciamentoPlantao.Models.Paginacao;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Models.ViewModels.Escala;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services.Escalas
{
    public class PostoEscalaService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogado;

        public PostoEscalaService(
            GerenciamentoPlantaoContext context,
            IUsuarioLogadoService usuarioLogado)
        {
            _context = context;
            _usuarioLogado = usuarioLogado;
        }

        // LISTAGEM
        public async Task<PagedResult<PostoEscala>> FindAllAsync(
            int paginaAtual,
            int tamanhoPagina,
            string? busca,
            string? ordenarPor,
            string? direcao,
            int? departamentoId)
        {
            IQueryable<PostoEscala> query = _context.PostosEscalas
                .AsNoTracking()
                .Include(p => p.Departamento);

            if (!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(p => p.Nome.Contains(busca));
            }

            if (departamentoId.HasValue)
            {
                query = query.Where(p =>
                    p.DepartamentoId == departamentoId.Value);
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

            return await PagedResult<PostoEscala>.CriarAsync(
                query,
                paginaAtual,
                tamanhoPagina);
        }

        // BUSCAR POR ID
        public async Task<PostoEscala> FindByIdAsync(
            int id,
            int departamentoId)
        {
            var posto = await _context.PostosEscalas
                .Include(p => p.Departamento)
                .Include(p => p.Horarios)
                .FirstOrDefaultAsync(p =>
                    p.Id == id &&
                    p.DepartamentoId == departamentoId);

            if (posto == null)
            {
                throw new NotFoundException(
                    "Posto não encontrado.");
            }

            return posto;
        }

        // BUSCAR POR ID COM AUDITORIA
        public async Task<PostoEscala> FindByIdWithAuditAsync(
            int id,
            int departamentoId)
        {
            var posto = await _context.PostosEscalas
                .AsNoTracking()
                .Include(p => p.Departamento)
                .Include(p => p.Horarios)
                .Include(p => p.UsuarioInsert)
                .Include(p => p.UsuarioUpdate)
                .Include(p => p.UsuarioInativacao)
                .FirstOrDefaultAsync(p =>
                    p.Id == id &&
                    p.DepartamentoId == departamentoId);

            if (posto == null)
            {
                throw new NotFoundException(
                    "Posto não encontrado.");
            }

            return posto;
        }

        // CRIAR POSTO E HORÁRIOS
        public async Task CriarPostoAsync(PostoEscalaFormViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            // VALIDAÇÃO DO POSTO
            if (string.IsNullOrWhiteSpace(vm.Nome))
            {
                throw new BusinessException("Informe o nome do posto.");
            }

            var nome = vm.Nome.Trim();

            if (nome.Length < 3 || nome.Length > 150)
            {
                throw new BusinessException(
                    "O nome do posto deve ter entre 3 e 150 caracteres.");
            }

            var departamentoExiste = await _context.Departamentos
                .AnyAsync(d =>
                    d.Id == vm.DepartamentoId &&
                    d.Ativo);

            if (!departamentoExiste)
            {
                throw new NotFoundException(
                    "Departamento não encontrado ou inativo.");
            }

            var nomeExiste = await _context.PostosEscalas
                .AnyAsync(p =>
                    p.Nome == nome &&
                    p.DepartamentoId == vm.DepartamentoId);

            if (nomeExiste)
            {
                throw new BusinessException(
                    "Já existe um posto com este nome neste departamento.");
            }

            // VALIDAÇÃO DOS HORÁRIOS
            if (vm.Horarios == null || vm.Horarios.Count == 0)
            {
                throw new BusinessException(
                    "Adicione pelo menos um horário ao posto.");
            }

            // Controle das combinações de nome e dia.
            var combinacoes = new HashSet<(string Nome, TipoDiaSemana Dia)>();

            foreach (var horarioVm in vm.Horarios)
            {
                if (string.IsNullOrWhiteSpace(horarioVm.Nome))
                {
                    throw new BusinessException(
                        "Todos os horários devem possuir um nome.");
                }

                var nomeHorario = horarioVm.Nome.Trim();

                if (nomeHorario.Length < 3 || nomeHorario.Length > 150)
                {
                    throw new BusinessException(
                        $"O nome do horário '{nomeHorario}' deve ter entre 3 e 150 caracteres.");
                }

                if (!horarioVm.HoraInicio.HasValue ||
                    !horarioVm.HoraFim.HasValue)
                {
                    throw new BusinessException(
                        $"Informe as horas inicial e final do horário '{nomeHorario}'.");
                }

                if (horarioVm.DiasSemana == null ||
                    horarioVm.DiasSemana.Count == 0)
                {
                    throw new BusinessException(
                        $"Selecione pelo menos um dia da semana para '{nomeHorario}'.");
                }

                foreach (var dia in horarioVm.DiasSemana)
                {
                    if (!Enum.IsDefined(typeof(TipoDiaSemana), dia))
                    {
                        throw new BusinessException(
                            $"O horário '{nomeHorario}' possui um dia da semana inválido.");
                    }

                    var chave = (nomeHorario.ToUpperInvariant(), dia);

                    if (!combinacoes.Add(chave))
                    {
                        throw new BusinessException(
                            $"O horário '{nomeHorario}' está duplicado no dia {dia}.");
                    }
                }
            }

            // CRIAÇÃO DO POSTO
            var posto = new PostoEscala(
                nome,
                vm.DepartamentoId,
                usuario.Id
            );

            // CRIAÇÃO DOS HORÁRIOS
            foreach (var horarioVm in vm.Horarios)
            {
                foreach (var dia in horarioVm.DiasSemana.Distinct())
                {
                    var horario = new BlocoHorarioEscala(
                        horarioVm.Nome.Trim(),
                        horarioVm.HoraInicio!.Value,
                        horarioVm.HoraFim!.Value,
                        dia,
                        posto.Id,
                        usuario.Id
                    );

                    posto.AddBlocoHorarioEscala(horario);
                }
            }

            _context.PostosEscalas.Add(posto);

            // Salva o posto e todos os horários em uma única operação.
            await _context.SaveChangesAsync();
        }

        // ATUALIZAR POSTO E HORÁRIOS

        public async Task AtualizarPostoAsync(
            EditarPostoEscalaViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            var posto = await FindByIdAsync(
                vm.Id,
                vm.DepartamentoId);

            if (string.IsNullOrWhiteSpace(vm.Nome))
                throw new BusinessException("Informe o nome do posto.");

            var nome = vm.Nome.Trim();

            if (nome.Length < 3 || nome.Length > 150)
                throw new BusinessException(
                    "O nome do posto deve ter entre 3 e 150 caracteres.");

            var nomeExiste = await _context.PostosEscalas
                .AnyAsync(p =>
                    p.Nome == nome &&
                    p.DepartamentoId == posto.DepartamentoId &&
                    p.Id != posto.Id);

            if (nomeExiste)
                throw new BusinessException(
                    "Já existe outro posto com este nome neste departamento.");

            // Continua utilizando a ViewModel individual na edição.
            ValidarHorarios(vm.Horarios);

            var idsInformados = vm.Horarios
                .Where(h => h.Id.HasValue)
                .Select(h => h.Id!.Value)
                .ToList();

            if (idsInformados.Count != idsInformados.Distinct().Count())
                throw new BusinessException(
                    "Existem horários repetidos no formulário.");

            var idsExistentes = posto.Horarios
                .Select(h => h.Id)
                .ToHashSet();

            if (idsInformados.Any(id => !idsExistentes.Contains(id)))
                throw new BusinessException(
                    "Um dos horários informados não pertence a este posto.");

            var horariosRemovidos = posto.Horarios
                .Where(h => !idsInformados.Contains(h.Id))
                .ToList();

            // Verifica as dependências dos horários existentes
            // em uma única consulta para cada relacionamento.
            var idsParaVerificar = posto.Horarios
                .Select(h => h.Id)
                .ToList();

            var idsEmRotacao = await _context.RegrasRotacaoHorarios
                .Where(r => idsParaVerificar.Contains(r.BlocoHorarioEscalaId))
                .Select(r => r.BlocoHorarioEscalaId)
                .Distinct()
                .ToListAsync();

            var idsEmEscalaBase = await _context.EscalasBaseItems
                .Where(e => idsParaVerificar.Contains(e.BlocoHorarioEscalaId))
                .Select(e => e.BlocoHorarioEscalaId)
                .Distinct()
                .ToListAsync();

            var idsVinculados = idsEmRotacao
                .Concat(idsEmEscalaBase)
                .ToHashSet();

            // BLOQUEIO DE EXCLUSÃO
            foreach (var horario in horariosRemovidos)
            {
                if (idsVinculados.Contains(horario.Id))
                {
                    throw new BusinessException(
                        $"O horário '{horario.Nome}' do dia " +
                        $"{horario.DiaSemana} não pode ser removido, " +
                        "pois está vinculado a uma regra de rotação ou escala-base.");
                }
            }

            // BLOQUEIO DE ALTERAÇÃO DE DIA E HORAS
            foreach (var horarioVm in vm.Horarios.Where(h => h.Id.HasValue))
            {
                var horario = posto.Horarios
                    .First(h => h.Id == horarioVm.Id!.Value);

                if (!idsVinculados.Contains(horario.Id))
                    continue;

                var alterouDia =
                    horario.DiaSemana != horarioVm.DiaSemana!.Value;

                var alterouInicio =
                    horario.HoraInicio != horarioVm.HoraInicio!.Value;

                var alterouFim =
                    horario.HoraFim != horarioVm.HoraFim!.Value;

                if (alterouDia || alterouInicio || alterouFim)
                {
                    throw new BusinessException(
                        $"O horário '{horario.Nome}' do dia " +
                        $"{horario.DiaSemana} está vinculado a uma " +
                        "regra de rotação ou escala-base. " +
                        "Somente o nome pode ser alterado.");
                }
            }

            // Todas as validações passaram.
            posto.Atualizar(nome, usuario.Id);

            foreach (var horarioVm in vm.Horarios)
            {
                if (horarioVm.Id.HasValue)
                {
                    var horario = posto.Horarios
                        .First(h => h.Id == horarioVm.Id.Value);

                    horario.Atualizar(
                        horarioVm.Nome.Trim(),
                        horarioVm.HoraInicio!.Value,
                        horarioVm.HoraFim!.Value,
                        horarioVm.DiaSemana!.Value,
                        usuario.Id);
                }
                else
                {
                    var novoHorario = new BlocoHorarioEscala(
                        horarioVm.Nome.Trim(),
                        horarioVm.HoraInicio!.Value,
                        horarioVm.HoraFim!.Value,
                        horarioVm.DiaSemana!.Value,
                        posto.Id,
                        usuario.Id);

                    posto.AddBlocoHorarioEscala(novoHorario);
                }
            }

            foreach (var horario in horariosRemovidos)
            {
                _context.BlocosHorarioEscala.Remove(horario);
            }

            await _context.SaveChangesAsync();
        }


        // VALIDAÇÃO DOS HORÁRIOS
        private static void ValidarHorarios(
            List<BlocoHorarioPostoViewModel> horarios)
        {
            if (horarios == null || !horarios.Any())
            {
                throw new BusinessException(
                    "Adicione pelo menos um horário ao posto.");
            }

            foreach (var horario in horarios)
            {
                if (string.IsNullOrWhiteSpace(horario.Nome))
                {
                    throw new BusinessException(
                        "Todos os horários devem possuir um nome.");
                }

                var nome = horario.Nome.Trim();

                if (nome.Length < 3 || nome.Length > 150)
                {
                    throw new BusinessException(
                        "O nome de cada horário deve ter entre 3 e 150 caracteres.");
                }

                if (!horario.HoraInicio.HasValue)
                {
                    throw new BusinessException(
                        $"Informe a hora inicial do horário '{nome}'.");
                }

                if (!horario.HoraFim.HasValue)
                {
                    throw new BusinessException(
                        $"Informe a hora final do horário '{nome}'.");
                }

                if (!horario.DiaSemana.HasValue ||
                    !Enum.IsDefined(horario.DiaSemana.Value))
                {
                    throw new BusinessException(
                        $"Selecione um dia válido para o horário '{nome}'.");
                }
            }

            // Impede nomes repetidos para o mesmo dia dentro do posto.
            var duplicidade = horarios
                .GroupBy(h => new
                {
                    Nome = h.Nome.Trim().ToUpperInvariant(),
                    Dia = h.DiaSemana!.Value
                })
                .FirstOrDefault(g => g.Count() > 1);

            if (duplicidade != null)
            {
                throw new BusinessException(
                    "Não é permitido cadastrar horários com o mesmo nome " +
                    "e dia da semana dentro do mesmo posto.");
            }

            // Observação:
            // HoraInicio == HoraFim é permitido.
            // Isso possibilita representar plantões de 24 horas.
        }

        // ATIVAR POSTO
        public async Task AtivarPostoAsync(
            int id,
            int departamentoId)
        {
            var usuario = await _usuarioLogado
                .ObterUsuarioLogadoAsync();

            var posto = await FindByIdAsync(
                id,
                departamentoId);

            posto.Ativar(usuario.Id);

            await _context.SaveChangesAsync();
        }

        // INATIVAR POSTO
        public async Task InativarPostoAsync(
            int id,
            int departamentoId)
        {
            var usuario = await _usuarioLogado
                .ObterUsuarioLogadoAsync();

            var posto = await FindByIdAsync(
                id,
                departamentoId);

            posto.Inativar(usuario.Id);

            await _context.SaveChangesAsync();
        }

        public async Task<HashSet<int>> ObterIdsHorariosVinculadosAsync(
    int postoEscalaId)
        {
            var idsRotacao = await _context.RegrasRotacaoHorarios
                .Where(r => r.BlocoHorarioEscala.PostoEscalaId == postoEscalaId)
                .Select(r => r.BlocoHorarioEscalaId)
                .ToListAsync();

            var idsEscalaBase = await _context.EscalasBaseItems
                .Where(e => e.BlocoHorarioEscala.PostoEscalaId == postoEscalaId)
                .Select(e => e.BlocoHorarioEscalaId)
                .ToListAsync();

            return idsRotacao
                .Concat(idsEscalaBase)
                .ToHashSet();
        }
    }
}