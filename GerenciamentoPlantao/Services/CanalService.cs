using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models;
using Microsoft.EntityFrameworkCore;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Paginacao;

namespace GerenciamentoPlantao.Services
{
    public class CanalService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogado;

        public CanalService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogado)
        {
            _context = context;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<PagedResult<Canal>> FindAllAsync(int paginaAtual, int tamanhoPagina, string? busca, string? ordenarPor, string? direcao, int? departamentoId)
        {
            IQueryable<Canal> query = _context.Canais
                .Include(c => c.Departamento);

            if (!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(c => c.Nome.Contains(busca));
            }
            if (departamentoId.HasValue)
            {
                query = query.Where(c => c.DepartamentoId == departamentoId.Value);
            }

            switch (ordenarPor)
            {
                case "nome":
                    query = direcao == "desc"
                        ? query.OrderByDescending(c => c.Nome)
                        : query.OrderBy(c => c.Nome);
                    break;

                case "departamento":
                    query = direcao == "desc"
                        ? query.OrderByDescending(c => c.Departamento.Nome)
                        : query.OrderBy(c => c.Departamento.Nome);
                    break;

                case "status":
                    query = direcao == "desc"
                        ? query.OrderByDescending(c => c.Ativo)
                        : query.OrderBy(c => c.Ativo);
                    break;

                default:
                    query = query.OrderBy(c => c.Nome);
                    break;
            }

            return await PagedResult<Canal>.CriarAsync(query, paginaAtual, tamanhoPagina);
        }

        public async Task<List<Canal>> FindAllActiveAsync()
        {
            return await _context.Canais
                .Where(c => c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<List<Canal>> FindAllDepartmentActiveAsync(int departamentoId)
        {
            return await _context.Canais
                .Where(c => c.Ativo && c.DepartamentoId == departamentoId)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Canal> FindByIdAsync(int id)
        {
            var canal = await _context.Canais
                .FirstOrDefaultAsync(x => x.Id == id);

            if (canal == null)
            {
                throw new NotFoundException("Canal não encontrado.");
            }

            return canal;
        }

        public async Task<Canal> FindByIdWithAuditAsync(int id)
        {
            var canal = await _context.Canais
                .Include(c => c.UsuarioInsert)
                .Include(c => c.UsuarioUpdate)
                .Include(c => c.UsuarioInativacao)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (canal == null)
            {
                throw new NotFoundException("Canal não encontrado.");
            }
            return canal;
        }

        public async Task CriarCanalAsync(CanalFormViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();
            var departamento = await _context.Departamentos
                    .Include(d => d.Canais)
                    .FirstOrDefaultAsync(d => d.Id == vm.DepartamentoId);

            if (departamento == null)
                throw new NotFoundException("Departamento não encontrado.");

            var canal = new Canal
            (
                vm.Nome,
                vm.DepartamentoId,
                usuario.Id
                
            );
            departamento.AddCanal(canal);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarCanalAsync(EditarCanalViewModel vm)
        {
            var canal = await FindByIdAsync(vm.Id);
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            var existe = await _context.Canais.AnyAsync(c =>  
                c.Id != vm.Id &&
                c.DepartamentoId == vm.DepartamentoId &&
                c.Nome == vm.Nome);

            if (existe)
            {
                throw new BusinessException("Já existe um canal com esse nome no departamento selecionado.");
            }

            canal.Atualizar
            (
                vm.Nome,
                vm.DepartamentoId,
                usuario.Id
            );
            await _context.SaveChangesAsync();
        }

        public async Task InativarCanalAsync(int id)
        {
            var canal = await FindByIdAsync(id);
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            canal.Inativar(usuario.Id);
            await _context.SaveChangesAsync();
        }

        public async Task AtivarCanalAsync(int id)
        {
            var canal = await FindByIdAsync(id);
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            canal.Ativar(usuario.Id);
            await _context.SaveChangesAsync();
        }
    }
}
