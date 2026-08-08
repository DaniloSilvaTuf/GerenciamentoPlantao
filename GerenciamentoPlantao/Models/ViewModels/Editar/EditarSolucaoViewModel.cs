using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarSolucaoViewModel : EditarViewModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da solução é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome da solução deve ter entre 3 e 200 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; } = null!;
    }
}
