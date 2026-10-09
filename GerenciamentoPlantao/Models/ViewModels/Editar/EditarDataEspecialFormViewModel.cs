using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarDataEspecialFormViewModel : EditarViewModelBase
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(150, MinimumLength = 3,
        ErrorMessage = "A descrição deve ter entre 3 e 150 caracteres.")]
        public string Descricao { get; set; } = string.Empty;
        
        public DateOnly NovaData { get; set; }
        public int DepartamentoId { get; set; }

        [ValidateNever]
        public string DepartamentoNome { get; set; } = null!;
    }
}
