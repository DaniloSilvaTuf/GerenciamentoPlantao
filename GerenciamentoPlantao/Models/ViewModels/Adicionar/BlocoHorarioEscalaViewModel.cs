using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class BlocoHorarioEscalaViewModel
    {
        [Required(ErrorMessage = "O nome do bloco é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome do bloco deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
        [Required(ErrorMessage = "Selecione o dia da semana.")]
        public List<TipoDiaSemana> DiasSemana { get; set; } = new();
        public IEnumerable<SelectListItem> Departamentos { get; set; } = Enumerable.Empty<SelectListItem>();
        
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um departamento.")]
        public int DepartamentoId { get; set; }
    }
}
