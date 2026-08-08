using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class CanalFormViewModel
    {
        [Required(ErrorMessage = "O nome do canal é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome do canal deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; } = null!;
    }
}
