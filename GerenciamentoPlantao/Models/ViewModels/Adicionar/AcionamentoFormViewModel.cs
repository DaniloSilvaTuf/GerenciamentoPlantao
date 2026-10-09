using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class AcionamentoFormViewModel
    {
        [Required(ErrorMessage = "A data do acionamento é obrigatória.")]
        [DataType(DataType.DateTime)]
        public DateTime DataAcionamento { get; set; }

        public string NomePlantonista { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma solução.")]
        public int CanalId { get; set; }
        public IEnumerable<SelectListItem>? Canal { get; set; } = Enumerable.Empty<SelectListItem>();

        public string? Acionador { get; set; }
        public int? NrAtendimento { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma estabelecimento.")]
        public int EstabelecimentoId { get; set; }
        public IEnumerable<SelectListItem> Estabelecimento { get; set; } = Enumerable.Empty<SelectListItem>();

        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma setor.")]
        public int SetorId { get; set; }
        public IEnumerable<SelectListItem> Setor { get; set; } = Enumerable.Empty<SelectListItem>();

        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma categoria.")]
        public int CategoriaId { get; set; }
        public IEnumerable<SelectListItem> Categoria { get; set; } = Enumerable.Empty<SelectListItem>();

        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma solução.")]
        public int SolucaoId { get; set; }
        public IEnumerable<SelectListItem> Solucao { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required(ErrorMessage = "O campo Apoio é obrigatório.")]
        public bool Apoio { get; set; }  
        public string? Observacao { get; set; }

        [Required(ErrorMessage = "O campo Status do acionamento é obrigatório.")]
        
        public IEnumerable<SelectListItem> Status = Enumerable.Empty<SelectListItem>();
        public StatusAcionamento StatusAcionamento { get; set; }
    }
}
