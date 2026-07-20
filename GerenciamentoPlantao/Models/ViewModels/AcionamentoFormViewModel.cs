using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class AcionamentoFormViewModel
    {
        public DateTime DataAcionamento { get; set; }
        public IEnumerable<SelectListItem>? Plantonista { get; set; }
        public int UsuarioId { get; set; }
        public IEnumerable<SelectListItem>? Canal { get; set; }
        public int CanalId { get; set; }
        public string? Acionador { get; set; }
        public int? NrAtendimento { get; set; }
        public IEnumerable<SelectListItem>? Estabelecimento { get; set; }
        public int EstabelecimentoId { get; set; }
        public IEnumerable<SelectListItem>? Setor { get; set; }
        public int SetorId { get; set; }
        public IEnumerable<SelectListItem>? Categoria { get; set; }
        public int CategoriaId { get; set; }
        public IEnumerable<SelectListItem>? Solucao { get; set; }
        public bool Apoio { get; set; }
        public int SolucaoId { get; set; }
        public string? Observacao { get; set; }
    }
}
