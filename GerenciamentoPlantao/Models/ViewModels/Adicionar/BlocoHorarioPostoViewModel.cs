using GerenciamentoPlantao.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Escala
{
    public class BlocoHorarioPostoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome do horário é obrigatório.")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a hora inicial.")]
        public TimeOnly? HoraInicio { get; set; }

        [Required(ErrorMessage = "Informe a hora final.")]
        public TimeOnly? HoraFim { get; set; }

        [Required(ErrorMessage = "Selecione o dia da semana.")]
        public TipoDiaSemana? DiaSemana { get; set; }

        public bool EstaVinculado { get; set; }
    }
}