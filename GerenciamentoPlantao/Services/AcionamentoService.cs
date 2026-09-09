using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Paginacao;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class AcionamentoService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogado;

        public AcionamentoService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogado)
        {
            _context = context;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<PagedResult<Acionamento>> FindAllAsync(
            int paginaAtual, int tamanhoPagina, string? ordenarPor, string? direcao, string? plantonistaId, int? estabelecimentoId, int? categoriaId, int? setorId, int? departamentoId)
        {
            IQueryable<Acionamento> query = _context.Acionamentos
                .Include(a => a.Canal)
                .Include(a => a.CategoriaAcionamento)
                .Include(a => a.Estabelecimento)
                .Include(a => a.Setor)
                .Include(a => a.Solucao)
                .Include(a => a.Plantonista);

            if(!string.IsNullOrWhiteSpace(plantonistaId))
            {
                query = query.Where(a => a.UsuarioId == plantonistaId);
            }

            if(estabelecimentoId.HasValue)
            {
                query = query.Where(a => a.EstabelecimentoId == estabelecimentoId.Value);
            }

            if(categoriaId.HasValue)
            {
                query = query.Where(a => a.CategoriaAcionamentoId == categoriaId.Value);
            }

            if(setorId.HasValue)
            {
                query = query.Where(a => a.SetorId == setorId.Value);
            }

            if(departamentoId.HasValue)
            {
                query = query.Where(a => a.DepartamentoId == departamentoId.Value);
            }

            switch (ordenarPor)
            {
                case "dataAcionamento":
                    query = direcao == "desc" 
                        ? query.OrderByDescending(a => a.DataAcionamento) 
                        : query.OrderBy(a => a.DataAcionamento);
                    break;

                case "canal":
                    query = direcao == "desc" 
                        ? query.OrderByDescending(a => a.Canal.Nome) 
                        : query.OrderBy(a => a.Canal.Nome);
                    break;

                case "categoria":
                    query = direcao == "desc" 
                        ? query.OrderByDescending(a => a.CategoriaAcionamento.Nome) 
                        : query.OrderBy(a => a.CategoriaAcionamento.Nome);
                    break;

                case "estabelecimento":
                    query = direcao == "desc" 
                        ? query.OrderByDescending(a => a.Estabelecimento.Nome) 
                        : query.OrderBy(a => a.Estabelecimento.Nome);
                    break;

                case "setor":
                    query = direcao == "desc" 
                        ? query.OrderByDescending(a => a.Setor.Nome) 
                        : query.OrderBy(a => a.Setor.Nome);
                    break;

                default:
                    query = query.OrderByDescending(a => a.DataAcionamento);
                    break;
            }

            return await PagedResult<Acionamento>.CriarAsync(query, paginaAtual, tamanhoPagina);

        }

        public async Task<Acionamento> FindByIdAsync(int id)
        {
            var acionamento = await _context.Acionamentos
                .Include(a => a.Canal)
                .Include(a => a.CategoriaAcionamento)
                .Include(a => a.Estabelecimento)
                .Include(a => a.Setor)
                .Include(a => a.Solucao)
                .Include(a => a.Plantonista)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (acionamento == null)
            {
                throw new NotFoundException("Acionamento não encontrado.");
            }   

            return acionamento;
        }

        public async Task<Acionamento> FindByIdWithAuditAsync(int id)
        {
            var acionamento = await _context.Acionamentos
                .Include(a => a.UsuarioInsert)
                .Include(a => a.UsuarioUpdate)
                .Include(a => a.UsuarioInativacao)
                .FirstOrDefaultAsync(a => a.Id == id);
            
            if (acionamento == null)
            {
                throw new NotFoundException("Acionamento não encontrado.");
            }
            return acionamento;
        }

        public async Task InserirAcionamentoAsync(AcionamentoFormViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();
            var acionamento = new Acionamento
            (
                vm.DataAcionamento,
                vm.CanalId,
                usuario.Id,
                usuario.DepartamentoId,
                vm.Acionador,
                vm.NrAtendimento,
                vm.EstabelecimentoId,
                vm.SetorId,
                vm.CategoriaId,
                vm.SolucaoId,
                vm.Apoio,
                vm.Observacao
            );

            _context.Acionamentos.Add(acionamento);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarAcionamentoAsync(EditarAcionamentoViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();
            var acionamento = await FindByIdAsync(vm.Id);

            if (acionamento.UsuarioId != usuario.Id)
            {
                throw new AccessDeniedException("Você não tem permissão para editar este acionamento.");
            }
            acionamento.AtualizarAcionamento(
                vm.DataAcionamento,
                vm.CanalId,
                vm.EstabelecimentoId,
                vm.SetorId,
                vm.CategoriaId,
                vm.Apoio,
                vm.SolucaoId,
                vm.Observacao,
                vm.Acionador,
                vm.NrAtendimento,
                DateTime.Now,
                usuario.Id
            );

            _context.Acionamentos.Update(acionamento);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAcionamentoAsync(int id)
        {

            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();
            var acionamento = await FindByIdAsync(id);

            if (acionamento.UsuarioId != usuario.Id)
            {
                throw new AccessDeniedException("Você não tem permissão para excluir este acionamento.");
            }

            _context.Acionamentos.Remove(acionamento);
            await _context.SaveChangesAsync();
        }
    }
}
