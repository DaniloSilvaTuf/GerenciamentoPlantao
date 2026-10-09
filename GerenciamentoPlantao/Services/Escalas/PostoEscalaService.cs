using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Exceptions;
using GerenciamentoPlantao.Models.Escala;
using GerenciamentoPlantao.Models.Paginacao;
using GerenciamentoPlantao.Models.ViewModels.Adicionar;
using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GerenciamentoPlantao.Services.Escalas
{
    public class PostoEscalaService
    {
        private readonly GerenciamentoPlantaoContext _context;
        private readonly IUsuarioLogadoService _usuarioLogado;

        public PostoEscalaService(GerenciamentoPlantaoContext context, IUsuarioLogadoService usuarioLogado)
        {
            _context = context;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<PagedResult<PostoEscala>> FindAllAsync(int paginaAtual, int tamanhoPagina, string? busca, string? ordenarPor, string? direcao, int? departamentoId)
        {
            IQueryable<PostoEscala> query = _context.PostosEscalas
                .Include(p => p.Departamento);

            if (!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(p => p.Nome.Contains(busca));
            }

            if (departamentoId.HasValue)
            {
                query = query.Where(p => p.DepartamentoId == departamentoId.Value);
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
            return await PagedResult<PostoEscala>.CriarAsync(query, paginaAtual, tamanhoPagina);
        }

        public async Task<PostoEscala> FindByIdAsync(int id, int departamentoId)
        {
            var posto = await _context.PostosEscalas
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(p => p.Id == id && p.DepartamentoId == departamentoId);

            if (posto == null)
            {
                throw new NotFoundException("Posto não encontrado.");
            }

            return posto;
        }

        public async Task<PostoEscala> FindByIdWithAuditAsync(int id, int departamentoId)
        {
            var posto = await _context.PostosEscalas
                .Include(p => p.Departamento)
                .Include(p => p.UsuarioInsert)
                .Include(p => p.UsuarioUpdate)
                .Include(p => p.UsuarioInativacao)
                .FirstOrDefaultAsync(p => p.Id == id && p.DepartamentoId == departamentoId);
            
            if (posto == null)
            {
                throw new NotFoundException("Posto não encontrado.");
            }
            
            return posto;
        }

        public async Task CriarPostoAsync(PostoEscalaFormViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            var departamento = await _context.Departamentos
                .Include(p => p.PostosEscalas)
                .FirstOrDefaultAsync(p => p.Id == vm.DepartamentoId);

            if (departamento == null)
                throw new NotFoundException("Departamento não encontrado.");

            var posto = new PostoEscala
            (
                vm.Nome.Trim(),
                vm.DepartamentoId,
                usuario.Id
            );

            departamento.AddPostoEscala(posto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarPostoAsync(EditarPostoEscalaViewModel vm)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();

            var posto = await FindByIdAsync(vm.Id, vm.DepartamentoId);

            var nomeExistente = await _context.PostosEscalas
                .AnyAsync(p => p.Nome == vm.Nome.Trim()
                            && p.DepartamentoId == vm.DepartamentoId
                            && p.Id != vm.Id);

            if (nomeExistente)
                throw new BusinessException("Já existe um posto com este nome neste departamento.");

            posto.Atualizar
                (
                    vm.Nome.Trim(),
                    usuario.Id
                );

            await _context.SaveChangesAsync();
        }

        public async Task AtivarPostoAsync(int id, int departamentoId)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();
            var posto = await FindByIdAsync(id, departamentoId);
            posto.Ativar(usuario.Id);
            await _context.SaveChangesAsync();
        }

        public async Task InativarPostoAsync(int id, int departamentoId)
        {
            var usuario = await _usuarioLogado.ObterUsuarioLogadoAsync();
            var posto = await FindByIdAsync(id, departamentoId);
            posto.Inativar(usuario.Id);
            await _context.SaveChangesAsync();
        }
    }
}
