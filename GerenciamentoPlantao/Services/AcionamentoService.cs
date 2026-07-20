using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class AcionamentoService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public AcionamentoService(GerenciamentoPlantaoContext context)
        {
            _context = context;
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

        public async Task<Acionamento?> FindByIdAsync(int id)
        {
            return await _context.Acionamentos
                .Include(a => a.Canal)
                .Include(a => a.CategoriaAcionamento)
                .Include(a => a.Estabelecimento)
                .Include(a => a.Setor)
                .Include(a => a.Solucao)
                .Include(a => a.Plantonista)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task InsertAsync(AcionamentoFormViewModel vm)
        {
            var acionamento = new Acionamento
            {
                DataRegistro = DateTime.Now,
                DataAcionamento = vm.DataAcionamento,
                UsuarioId = vm.UsuarioId,
                CanalId = vm.CanalId,
                Acionador = vm.Acionador,
                NrAtendimento = vm.NrAtendimento,
                EstabelecimentoId = vm.EstabelecimentoId,
                SetorId = vm.SetorId,
                CategoriaAcionamentoId = vm.CategoriaId,
                SolucaoId = vm.SolucaoId,
                Apoio = vm.Apoio,
                Observacao = vm.Observacao,
            };

            _context.Acionamentos.Add(acionamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditarAcionamentoViewModel vm)
        {
            var acionamento = await _context.Acionamentos.FindAsync(vm.Id);
            if (acionamento == null)
            {
                throw new Exception("Acionamento não encontrado.");
            }
            acionamento.AtualizarAcionamento(
                vm.DataAcionamento,
                vm.CanalId,
                vm.UsuarioId,
                vm.EstabelecimentoId,
                vm.SetorId,
                vm.CategoriaId,
                vm.Apoio,
                vm.SolucaoId,
                vm.Observacao,
                vm.Acionador,
                vm.NrAtendimento
            );
            _context.Acionamentos.Update(acionamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var acionamento = await _context.Acionamentos.FindAsync(id);
            if (acionamento == null)
            {
                throw new Exception("Acionamento não encontrado.");
            }
            _context.Acionamentos.Remove(acionamento);
            await _context.SaveChangesAsync();
        }
    }
}
