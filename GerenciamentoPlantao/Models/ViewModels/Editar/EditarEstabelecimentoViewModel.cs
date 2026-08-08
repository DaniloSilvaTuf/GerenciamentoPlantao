using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarEstabelecimentoViewModel : EditarViewModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do canal é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome do canal deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public bool Ativo { get; set; }
    }
}
