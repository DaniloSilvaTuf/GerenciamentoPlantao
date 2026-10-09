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

        public BlocoHorarioEscalaService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogado)
        {
            _context = context;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<PagedResult<BlocoHorarioEscala>> FindAllAsync(int paginaAtual, int tamanhoPagina, string? busca, string? ordenarPor, string? direcao, int? departamentoId, TipoDiaSemana? diaSemana)
        {
            IQueryable<BlocoHorarioEscala> query = _context.BlocosHorarioEscala
                .Include(b => b.Departamento);

            if(!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(b => b.Nome.Contains(busca));
            }

            if(departamentoId.HasValue)
            {
                query = query.Where(b => b.DepartamentoId == departamentoId.Value);
            }

            if(diaSemana.HasValue)
            {
                query = query.Where(b => b.DiaSemana == diaSemana.Value);
            }

            switch(ordenarPor)
            {
                case "nome":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.Nome)
                        : query.OrderBy(b => b.Nome);
                    break;
                case "departamento":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.Departamento.Nome)
                        : query.OrderBy(b => b.Departamento.Nome);
                    break;
                case "ativo":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.Ativo)
                        : query.OrderBy(b => b.Ativo);
                    break;
                case "dia":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.DiaSemana)
                        : query.OrderBy(b => b.DiaSemana);
                    break;
                default:
                    query = query.OrderBy(b => b.Id);
                    break;
            }
            return await PagedResult<BlocoHorarioEscala>.CriarAsync(query, paginaAtual, tamanhoPagina);
        }

        public async Task<BlocoHorarioEscala> FindByIdAsync (int id, int departamentoId)
        {
            var bloco = await _context.BlocosHorarioEscala
                .Include(b => b.Departamento)
                .FirstOrDefaultAsync(b => b.Id == id && b.DepartamentoId == departamentoId);

            if(bloco == null)
            {
                throw new NotFoundException("Bloco de horário não encontrado.");
            }

            return bloco;
        }

        public async Task<BlocoHorarioEscala> FindByIdWithAuditAsync(int id, int departamentoId)
        {
            var bloco = await _context.BlocosHorarioEscala
                .Include(b => b.Departamento)
                .Include(b => b.UsuarioInsert)
                .Include(b => b.UsuarioUpdate)
                .Include(b => b.UsuarioInativacao)
                .FirstOrDefaultAsync(b => b.Id == id && b.DepartamentoId == departamentoId);

            if(bloco == null)
            {
                throw new NotFoundException("Bloco de horário não encontrado.");
            }

            return bloco;
        }

        public async Task InserirBlocoHorarioAsync(BlocoHorarioEscalaViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            var departamento = await _context.Departamentos
                .Include(b => b.BlocosHoarioEscala)
                .FirstOrDefaultAsync(b => b.Id == vm.DepartamentoId);

            if (departamento == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            foreach (var dia in vm.DiasSemana)
            {

                var bloco = new BlocoHorarioEscala
                    (
                        vm.Nome.Trim(),
                        vm.HoraInicio,
                        vm.HoraFim,
                        dia,
                        vm.DepartamentoId,
                        usuario.Id
                    );

                departamento.AddBlocoHorarioEscala(bloco);
            }
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarBlocoHorarioAsync(EditarBlocoHorarioEscalaViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            var bloco = await FindByIdAsync(vm.Id, vm.DepartamentoId);

            var nomeExiste = await _context.BlocosHorarioEscala
                .AnyAsync(b => b.Nome == vm.Nome.Trim()
                            && b.DepartamentoId == vm.DepartamentoId
                            && b.DiaSemana == vm.DiaSemana.Value
                            && b.Id != vm.Id);

            if (nomeExiste)
            {
                throw new BusinessException("Já existe um bloco com este nome neste departamento e dia da semana.");
            }

            bloco.Atualizar
                (
                    vm.Nome.Trim(),
                    vm.HoraInicio,
                    vm.HoraFim,
                    vm.DiaSemana.Value,
                    usuario.Id
                );

            await _context.SaveChangesAsync();
        }

        public async Task AtivarBlocoHorarioEscalaAsync(int id, int departamentoId)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();
            var bloco = await FindByIdAsync(id, departamentoId);

            bloco.Ativar(usuario.Id);
            await _context.SaveChangesAsync();
        }

        public async Task InativarBlocoHorarioEscalaAsync(int id, int departamentoId)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();
            var bloco = await FindByIdAsync(id, departamentoId);

            bloco.Inativar(usuario.Id);
            await _context.SaveChangesAsync();
        }
    }
}