using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarSetorViewModel : EditarViewModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do setor é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome do setor deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        
        public bool Ativo { get; set; }
        public int EstabelecimentoId { get; set; }
        public IEnumerable<SelectListItem> Estabelecimentos { get; set; } = null!;
    }

}