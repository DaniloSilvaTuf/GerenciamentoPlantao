namespace GerenciamentoPlantao.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalAcionamentos { get; set; }

        public List<GraficoItemViewModel> PorHora { get; set; } = new();
        public List<GraficoItemViewModel> PorDiaSemana { get; set; } = new();
        public List<GraficoItemViewModel> PorMes { get; set; } = new();
        public List<GraficoItemViewModel> PorCategoria { get; set; } = new();
        public List<GraficoItemViewModel> PorSetor { get; set; } = new();
    }

    public class GraficoItemViewModel
    {
        public string Label { get; set; } = string.Empty;
        public int Valor { get; set; }
    }
}