using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPlantao.Services
{
    public class DashboardService
    {
        private readonly GerenciamentoPlantaoContext _context;

        public DashboardService(GerenciamentoPlantaoContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> ObterDashboardAsync()
        {
            var vm = new DashboardViewModel
            {
                PorHora = await ObterAcionamentosPorHoraAsync(),
            };

            return vm;
        }

        public async Task<List<GraficoItemViewModel>> ObterAcionamentosPorHoraAsync()
        {    
            var dadosBanco = await _context.Acionamentos
                .GroupBy(a => a.DataAcionamento.Hour)
                .Select(g => new 
                {
                    Hora = g.Key,
                    Valor = g.Count()
                })
                .OrderBy(x => x.Hora)
                .ToListAsync();

            var horas = Enumerable.Range(0, 24);

            var dados = horas
                .Select(h => new GraficoItemViewModel
                {
                    Label = $"{h:00}h",
                    Valor = dadosBanco
                        .FirstOrDefault(x => x.Hora == h)?.Valor?? 0
                })
                .ToList();

            return dados;
        }
    }
}
