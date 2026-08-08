using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;
using GerenciamentoPlantao.Controllers;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;

namespace GerenciamentoPlantao.Services
{
    public class SetorService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogadoService;

        public SetorService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogadoService)
        {
            _context = context;
            _usuarioLogadoService = usuarioLogadoService;
        }

        public async Task<List<Setor>> FindAllAsync()
        {
            return await _context.Setores
                .Include(s => s.Estabelecimento)
                .ToListAsync();
        }

        public async Task<List<Setor>> FindAllActiveAsync()
        {
            return await _context.Setores
                .Where(s => s.Ativo)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<Setor> FindByIdAsync(int id)
        {
            var setor = await _context.Setores.FirstOrDefaultAsync(x => x.Id == id);
            
            if (setor == null)
                throw new Exception("Setor não encontrado.");
            return setor;
        }

        public async Task<Setor> FindByIdWithAuditAsync(int id)
        {
            var setor = await _context.Setores
                .Include(s => s.UsuarioInsert)
                .Include(s => s.UsuarioUpdate)
                .Include(s => s.UsuarioInativacao)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (setor == null)
            {
                throw new Exception("Setor não encontrado.");
            }
            return setor;
        }

        public async Task InserirSetorAsync(SetorFormViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var estabelecimento = await _context.Estabelecimentos
                    .Include(e => e.Setores)
                    .FirstOrDefaultAsync(e => e.Id == vm.EstabelecimentoId);

            if (estabelecimento == null)
                throw new Exception("Estabelecimento não encontrado.");

            var setor = new Setor
                (
                    vm.Nome,
                    vm.EstabelecimentoId,
                    usuario.Id
                );

            estabelecimento.AddSetor(setor);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarSetorAsync(EditarSetorViewModel vm)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var setor = await FindByIdAsync(vm.Id);

            var existe = await _context.Setores.AnyAsync(s => 
                s.Id != vm.Id &&
                s.EstabelecimentoId == vm.EstabelecimentoId &&
                s.Nome == vm.Nome);

            if (existe)
            {
                throw new Exception("Já existe um setor com o mesmo nome neste estabelecimento.");
            }

            setor.AtualizarSetor
                (
                    vm.Nome,
                    vm.EstabelecimentoId,
                    usuario.Id
                );

            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var setor = await FindByIdAsync(id);

            setor.Inativar(usuario.Id);
            await _context.SaveChangesAsync();
        }

        public async Task AtivarAsync(int id)
        {
            var usuario = await _usuarioLogadoService.ObterUsuarioLogadoAsync();
            var setor = await FindByIdAsync(id);

            setor.Ativar(usuario.Id);
            await _context.SaveChangesAsync();
        }
    }
}