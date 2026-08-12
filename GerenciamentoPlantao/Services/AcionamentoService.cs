using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models;
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

        public async Task<List<Acionamento>> FindAllAsync()
        {
            return await _context.Acionamentos
                .Include(a => a.Canal)
                .Include(a => a.CategoriaAcionamento)
                .Include(a => a.Estabelecimento)
                .Include(a => a.Setor)
                .Include(a => a.Solucao)
                .Include(a => a.Plantonista)
                .OrderByDescending(x => x.DataAcionamento)
                .ToListAsync();
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
