namespace GerenciamentoPlantao.Models.ViewModels
{
    public class PaginacaoViewModel
    {
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalItens { get; set; }
        public bool TemPaginaAnterior => PaginaAtual > 1;
        public bool TemProximaPagina => PaginaAtual < TotalPaginas;
        public string Action { get; set; } = "Index";
        public string? Controller { get; set; }
        public Dictionary<string, string?> ParametrosRota { get; set; } = new();
    }
}
