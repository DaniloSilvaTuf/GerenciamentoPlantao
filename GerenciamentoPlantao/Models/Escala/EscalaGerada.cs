using GerenciamentoPlantao.Exceptions;

namespace GerenciamentoPlantao.Models.Escala
{
    public class EscalaGerada : EntidadeBase
    {
        public int Id { get; set; }
        public EscalaBase EscalaBase { get; set; } = null!;
        public int EscalaBaseId { get; set; }
        public Departamento Departamento { get; set; } = null!;
        public int DepartamentoId { get; set; }
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
        public ICollection<EscalaGeradaItem> EscalaGeradaItems { get; set; } = new List<EscalaGeradaItem>();

        public EscalaGerada(int escalaBaseId, int departamentoId, DateOnly dataInicio, DateOnly dataFim, string usuarioId)
        {
            if(dataInicio.DayOfWeek != DayOfWeek.Monday)
            {
                throw new BusinessException("A data de início da escala gerada deve ser uma segunda-feira.");
            }

            if(dataInicio > dataFim)
            {
                throw new BusinessException("A data de fim da escala gerada não pode ser anterior à data de início.");
            }

            EscalaBaseId = escalaBaseId;
            DepartamentoId = departamentoId;
            DataInicio = dataInicio;
            DataFim = dataFim;
            RegistrarCriacao(usuarioId);
        }

        public EscalaGerada() { }
    }
}
