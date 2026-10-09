using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class DataEspecialEscalaFormViewModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "A descrição deve ter entre 3 e 150 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a data especial.")]
        [DataType(DataType.Date)]
        public DateOnly Data { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione um departamento.")]
        public int DepartamentoId { get; set; }

        public IEnumerable<SelectListItem> Departamentos { get; set; }
            = Enumerable.Empty<SelectListItem>();
    }
}