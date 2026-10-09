using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class PostoEscalaFormViewModel
    {
        [Required(ErrorMessage = "O nome do posto é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome do posto deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um departamento.")]
        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
