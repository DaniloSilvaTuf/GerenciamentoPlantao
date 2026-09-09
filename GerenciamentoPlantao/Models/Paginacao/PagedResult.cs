using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace GerenciamentoPlantao.Models.Paginacao
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int PaginaAtual { get; set; } //o número da página que eu estou no momento
        public int TamanhoPagina { get; set; } //quantidade de registros que uma página vai ter
        public int TotalItens { get; set; } //quantidade Total de Itens
        public int TotalPaginas => (int)Math.Ceiling((double)TotalItens / TamanhoPagina); //quantidade total de itens divididos pela quantidade de itens que cabem em uma página;
        public bool TemPaginaAnterior => PaginaAtual > 1;
        public bool TemProximaPagina => PaginaAtual < TotalPaginas;


        public static async Task<PagedResult<T>> CriarAsync(IQueryable<T> query, int paginaAtual, int tamanhoPagina)
        {
            var totalItens = await query.CountAsync();
            var skip = (paginaAtual - 1) * tamanhoPagina;

            var items = await query
                .Skip(skip)
                .Take(tamanhoPagina)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                PaginaAtual = paginaAtual,
                TamanhoPagina = tamanhoPagina,
                TotalItens = totalItens
            };
        }
    }
}

