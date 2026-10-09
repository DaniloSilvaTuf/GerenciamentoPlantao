using GerenciamentoPlantao.Exceptions;

namespace GerenciamentoPlantao.Models.Escala
{
    public class EscalaBase : EntidadeBase
    {
        public int Id { get; set; }
        public DateOnly DataInicio { get; set; }
        public Departamento Departamento { get; set; } = null!;
        public int DepartamentoId { get; set; }
        public ICollection<EscalaBaseItem> EscalaBaseItems { get; set; } = new List<EscalaBaseItem>();
        public ICollection<EscalaBaseRegraRotacao> RegrasRotacao { get; set; } = new List<EscalaBaseRegraRotacao>();

        public EscalaBase(DateOnly dataInicio, int departamentoId, string usuarioId)
        {
            if (dataInicio.DayOfWeek != DayOfWeek.Monday)
            {
                throw new BusinessException("A data de início da escala base deve ser uma segunda-feira.");
            }

            DataInicio = dataInicio;
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }

        public EscalaBase() { }
    }
}
