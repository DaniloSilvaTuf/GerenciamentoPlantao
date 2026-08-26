using GerenciamentoPlantao.Data;
using GerenciamentoPlantao.Models.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
                PorDiaSemana = await ObterAcionamentosPorDiaAsync(),
                PorMes = await ObterAcionamentosPorMesAsync(),
                PorCategoria = await ObterAcionamentosPorCategoriaAsync(),
                PorSetor = await ObterAcionamentosPorSetorAsync(),
                PorEstabelecimento = await ObterAcionamentosPorEstabelecimentoAsync(),
                ComApoio = await ObterAcionamentosComApoioAsync(),

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
                    Valor = dadosBanco.FirstOrDefault(x => x.Hora == h)?.Valor?? 0
                })
                .ToList();

            return dados;
        }

        public async Task<List<GraficoItemViewModel>> ObterAcionamentosPorDiaAsync()
        {
            var dadosBanco = await _context.Acionamentos
                .Select(a => a.DataAcionamento)
                .ToListAsync();

            var dadosAgrupados = dadosBanco.GroupBy(d => d.DayOfWeek)
                .Select(g => new
                {
                    DiaSemana = g.Key,
                    Valor = g.Count()
                })
                .OrderBy(x => x.DiaSemana)
                .ToList();

            var dias = Enumerable.Range(1, 6).Append(0);

            var dados = dias
                .Select(d => new GraficoItemViewModel
                {
                    Label = (DayOfWeek)d switch
                    { DayOfWeek.Monday => "Seg",
                        DayOfWeek.Tuesday => "Ter",
                        DayOfWeek.Wednesday => "Qua",
                        DayOfWeek.Thursday => "Qui",
                        DayOfWeek.Friday => "Sex",
                        DayOfWeek.Saturday => "Sáb",
                        DayOfWeek.Sunday => "Dom",
                        _ => "Inválido"
                    },
                    Valor = dadosAgrupados.FirstOrDefault(x => x.DiaSemana == (DayOfWeek)d)?.Valor ?? 0
                })
                .ToList();

            return dados;
        }

        public async Task<List<GraficoItemViewModel>> ObterAcionamentosPorMesAsync()
        {
            var dadosBanco = await _context.Acionamentos
                .Select(a => a.DataAcionamento)
                .Where(a => a.Year == DateTime.Now.Year)
                .ToListAsync();

            var dadosAgrupados = dadosBanco.GroupBy(d => d.Month)
                .Select(g => new
                {
                    Mes = g.Key,
                    Valor = g.Count()
                })
                .OrderBy(x => x.Mes)
                .ToList();

            var meses = Enumerable.Range(1, 12);

            var dados = meses
                .Select(m => new GraficoItemViewModel
                {
                    Label = new DateTime(DateTime.Now.Year, m, 1).ToString("MMM", new CultureInfo("pt-BR")),
                    Valor = dadosAgrupados.FirstOrDefault(x => x.Mes == m)?.Valor ?? 0
                })
                .ToList();

            return dados;
        }

        public async Task<List<GraficoItemViewModel>> ObterAcionamentosPorCategoriaAsync()
        {
            var anoAtual = DateTime.Now.Year;
            var dadosBanco = await _context.Acionamentos
                .Where(a => a.DataAcionamento.Year == anoAtual)
                .GroupBy(a => new
                {
                    a.CategoriaAcionamentoId,
                    a.CategoriaAcionamento.Nome
                })
                .Select(g => new GraficoItemViewModel
                {
                    Label = g.Key.Nome,
                    Valor = g.Count()
                })
                .OrderByDescending(o => o.Valor)
                .ToListAsync();

            return dadosBanco;
        }

        public async Task<List<GraficoItemViewModel>> ObterAcionamentosPorSetorAsync()
        {
            var anoAtual = DateTime.Now.Year;
            var dadosBanco = await _context.Acionamentos
                .Where(a => a.DataAcionamento.Year == anoAtual)
                .GroupBy(a => new
                {
                    a.SetorId,
                    a.Setor.Nome
                })
                .Select(g => new GraficoItemViewModel
                {
                    Label = g.Key.Nome,
                    Valor = g.Count()
                })
                .OrderByDescending(o => o.Valor)
                .ToListAsync();

            return dadosBanco;
        }

        public async Task<List<GraficoItemViewModel>> ObterAcionamentosPorEstabelecimentoAsync()
        {
            var anoAtual = DateTime.Now.Year;
            var dadosBanco = await _context.Acionamentos
                .Where(a => a.DataAcionamento.Year == anoAtual)
                .GroupBy(a => new
                {
                    a.EstabelecimentoId,
                    a.Estabelecimento.Nome
                })
                .Select(g => new GraficoItemViewModel
                {
                    Label = g.Key.Nome,
                    Valor = g.Count()
                })
                .OrderByDescending(o => o.Valor)
                .ToListAsync();

            return dadosBanco;
        }

        public async Task<List<GraficoItemViewModel>> ObterAcionamentosComApoioAsync()
        {
            var anoAtual = DateTime.Now.Year;
            var dadosBanco = await _context.Acionamentos
                .Where(a => a.DataAcionamento.Year == anoAtual)
                .GroupBy(a => a.Apoio)
                .Select(g => new GraficoItemViewModel
                {
                    Label = g.Key ? "Sim" : "Não",
                    Valor = g.Count()
                })
                .ToListAsync();

            return dadosBanco;
        }
    }
}
