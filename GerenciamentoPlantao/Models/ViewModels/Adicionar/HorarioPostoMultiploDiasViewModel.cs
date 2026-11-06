using GerenciamentoPlantao.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Escala
{
    public class HorarioPostoMultiploDiasViewModel
    {
        [Required(ErrorMessage = "Informe o nome do horário.")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a hora inicial.")]
        public TimeOnly? HoraInicio { get; set; }

        [Required(ErrorMessage = "Informe a hora final.")]
        public TimeOnly? HoraFim { get; set; }

        [MinLength(1, ErrorMessage = "Selecione pelo menos um dia da semana.")]
        public List<TipoDiaSemana> DiasSemana { get; set; } = new();
    }
}