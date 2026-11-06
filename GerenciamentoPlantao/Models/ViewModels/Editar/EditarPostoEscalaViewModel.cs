using GerenciamentoPlantao.Models.ViewModels.Escala;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarPostoEscalaViewModel : EditarViewModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do posto é obrigatório.")]
        [StringLength(
            150,
            MinimumLength = 3,
            ErrorMessage = "O nome do posto deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public bool Ativo { get; set; }

        public int DepartamentoId { get; set; }

        public string DepartamentoNome { get; set; } = string.Empty;

        // Horários do posto
        public List<BlocoHorarioPostoViewModel> Horarios { get; set; }
            = new();
    }
}