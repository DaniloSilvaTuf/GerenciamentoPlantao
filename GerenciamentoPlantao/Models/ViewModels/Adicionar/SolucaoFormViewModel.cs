using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class SolucaoFormViewModel
    {
        [Required(ErrorMessage = "O nome da solução é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome da solução deve ter entre 3 e 200 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; } = null!;
    }
}
