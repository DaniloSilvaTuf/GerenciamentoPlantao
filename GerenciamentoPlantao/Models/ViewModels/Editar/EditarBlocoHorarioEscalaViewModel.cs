using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarBlocoHorarioEscalaViewModel : EditarViewModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do bloco é obrigatório.")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "O nome do bloco deve ter entre 3 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public bool Ativo { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFim { get; set; }

        [Required(ErrorMessage = "Selecione o dia da semana.")]
        public TipoDiaSemana? DiaSemana { get; set; }

        public int DepartamentoId { get; set; }

        [ValidateNever]
        public string DepartamentoNome { get; set; } = string.Empty;
    }
}