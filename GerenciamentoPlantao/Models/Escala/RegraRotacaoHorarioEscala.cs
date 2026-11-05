namespace GerenciamentoPlantao.Models.Escala
{
    public class RegraRotacaoHorarioEscala : EntidadeBase
    {
        public int Id { get; set; }
        public int OrdemRotacao { get; set; }

        public RegraRotacaoEscala RegraRotacao { get; set; } = null!;
        public int RegraRotacaoId { get; set; }

        public BlocoHorarioEscala BlocoHorarioEscala { get; set; } = null!;
        public int BlocoHorarioEscalaId { get; set; }

        public RegraRotacaoHorarioEscala(
            int ordemRotacao,
            int regraRotacaoId,
            int blocoHorarioEscalaId,
            string usuarioId)
        {
            OrdemRotacao = ordemRotacao;
            RegraRotacaoId = regraRotacaoId;
            BlocoHorarioEscalaId = blocoHorarioEscalaId;

            RegistrarCriacao(usuarioId);
        }

        public RegraRotacaoHorarioEscala() { }

        public void AtualizarRegraRotacaoHorarioEscala(
            int ordemRotacao,
            int blocoHorarioEscalaId,
            string usuarioId)
        {
            OrdemRotacao = ordemRotacao;
            BlocoHorarioEscalaId = blocoHorarioEscalaId;

            RegistrarAtualizacao(usuarioId);
        }
    }
}