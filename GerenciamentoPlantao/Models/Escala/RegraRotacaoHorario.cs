using GerenciamentoPlantao.Models.Enums;

namespace GerenciamentoPlantao.Models.Escala
{
    public class RegraRotacaoHorario : EntidadeBase
    {
        public int Id { get; set; }
        public TipoDiaSemana DiaSemana { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeSpan Duracao { get; set; }
        public RegraRotacao RegraRotacao { get; set; } = null!;
        public int RegraRotacaoId { get; set; }

        public RegraRotacaoHorario(TipoDiaSemana diaSemana, TimeOnly horaInicio, TimeSpan duracao, int regraRotacaoId, string usuarioId)
        {
            DiaSemana = diaSemana;
            HoraInicio = horaInicio;
            Duracao = duracao;
            RegraRotacaoId = regraRotacaoId;
            RegistrarCriacao(usuarioId);
        }

        public RegraRotacaoHorario() { }
    }
}
