using GerenciamentoPlantao.Models.ViewModels.Editar;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class CategoriaFormViewModel
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome da categoria deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; } = null!;
    }
}
