namespace GerenciamentoPlantao.Models.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public int TotalAcionamentos { get; set; }

        public List<GraficoItemViewModel> PorHora { get; set; } = new();
        public List<GraficoItemViewModel> PorDiaSemana { get; set; } = new();
        public List<GraficoItemViewModel> PorMes { get; set; } = new();
        public List<GraficoItemViewModel> PorCategoria { get; set; } = new();
        public List<GraficoItemViewModel> PorSetor { get; set; } = new();
        public List<GraficoItemViewModel> PorEstabelecimento { get; set; } = new();
        public List<GraficoItemViewModel> ComApoio { get; set; } = new();

    }

    public class GraficoItemViewModel
    {
        public string Label { get; set; } = string.Empty;
        public int Valor { get; set; }
    }
}