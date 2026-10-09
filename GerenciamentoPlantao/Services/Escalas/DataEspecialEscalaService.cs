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
    public class DataEspecialEscalaService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogado;

        public DataEspecialEscalaService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogado)
        {
            _context = context;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<PagedResult<DataEspecialEscala>> FindAllAsync(int paginaAtual, int tamanhoPagina, string? busca, string? ordenarPor, string? direcao, int? departamentoId)
        {
            IQueryable<DataEspecialEscala> query = _context.DatasEspeciaisEscala
                .AsNoTracking()
                .Include(d => d.Departamento);

            if (!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(b => b.Descricao.Contains(busca));
            }

            if (departamentoId.HasValue)
            {
                query = query.Where(b => b.DepartamentoId == departamentoId.Value);
            }

            switch (ordenarPor)
            {
                case "descricao":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.Descricao)
                        : query.OrderBy(b => b.Descricao);
                    break;
                case "departamento":
                    query = direcao == "desc"
                        ? query.OrderByDescending(b => b.Departamento.Nome)
                        : query.OrderBy(b => b.Departamento.Nome);
                    break;
                case "data":
                    query = direcao == "desc"
                        ? query.OrderByDescending(d => d.Data)
                        : query.OrderBy(d => d.Data);
                    break;
                default:
                    query = query.OrderBy(b => b.Id);
                    break;
            }
            return await PagedResult<DataEspecialEscala>.CriarAsync(query, paginaAtual, tamanhoPagina);
        }

        public async Task<DataEspecialEscala> FindByIdAsync(int id, int departamentoId)
        {
            var dataEspecial = await _context.DatasEspeciaisEscala
                .Include(d => d.Departamento)
                .FirstOrDefaultAsync(d => d.Id == id && d.DepartamentoId == departamentoId);
            
            if(dataEspecial == null)
            {
                throw new NotFoundException("Data especial não encontrada.");
            }

            return dataEspecial;
        }

        public async Task<DataEspecialEscala> FindByIdWithAuditAsync(int id, int departamentoId)
        {
            var dataEspecial = await _context.DatasEspeciaisEscala
                .Include(d => d.Departamento)
                .Include(d => d.UsuarioInsert)
                .Include(d => d.UsuarioUpdate)
                .FirstOrDefaultAsync(d => d.Id == id && d.DepartamentoId == departamentoId);

            if (dataEspecial == null)
            {
                throw new NotFoundException("Data especial não encontrada.");
            }

            return dataEspecial;
        }

        public async Task CriarDataEspecialEscalaAsync(DataEspecialEscalaFormViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            var departamento = await _context.Departamentos
                .Include(d => d.DatasEspeciaisEscalas)
                .FirstOrDefaultAsync(d => d.Id == vm.DepartamentoId);

            if (departamento == null)
            {
                throw new NotFoundException("Departamento não encontrado.");
            }

            var dataEspecial = new DataEspecialEscala
                (
                    vm.Descricao.Trim(),
                    vm.Data,
                    vm.DepartamentoId,
                    usuario.Id
                );
            departamento.AddDataEspecialEscala(dataEspecial);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarDataEspecialEscalaAsync(EditarDataEspecialFormViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            var dataEspecial = await FindByIdAsync(vm.Id, vm.DepartamentoId);

            var dataExiste = await _context.DatasEspeciaisEscala
                .AnyAsync(d => d.Data == vm.NovaData
                            && d.DepartamentoId == vm.DepartamentoId
                            && d.Id != vm.Id);

            if (dataExiste)
            {
                throw new BusinessException("Já existe uma data especial cadastrada para o mesmo departamento.");
            }

            dataEspecial.Atualizar(
                vm.NovaData, 
                vm.Descricao.Trim(),
                usuario.Id);

            await _context.SaveChangesAsync();
        }
    }
}
