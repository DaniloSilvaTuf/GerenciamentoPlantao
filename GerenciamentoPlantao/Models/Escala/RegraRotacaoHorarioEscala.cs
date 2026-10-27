namespace GerenciamentoPlantao.Models.Escala
{
    public class RegraRotacaoHorarioEscala : EntidadeBase
    {
        public int Id { get; set; }
        public int OrdemRotacao { get; set; }
        public RegraRotacaoEscala RegraRotacao { get; set; } = null!;
        public int RegraRotacaoId { get; set; }
        public BlocoHorarioPostoEscala BlocoHorarioPostoEscala { get; set; } = null!;
        public int BlocoHorarioPostoEscalaId { get; set; }


        public RegraRotacaoHorarioEscala(int ordemRotacao, int regraRotacaoId, int blocoHorarioPostoEscalaId, string usuarioId)
        {
            OrdemRotacao = ordemRotacao;
            RegraRotacaoId = regraRotacaoId;
            BlocoHorarioPostoEscalaId = blocoHorarioPostoEscalaId;
            RegistrarCriacao(usuarioId);
        }

        public RegraRotacaoHorarioEscala() { }

        public void AtualizarRegraRotacaoHorarioEscala(int ordemRotacao, int blocoHorarioPostoEscalaId, string usuarioId)
        {
            OrdemRotacao = ordemRotacao;
            BlocoHorarioPostoEscalaId = blocoHorarioPostoEscalaId;
            RegistrarAtualizacao(usuarioId);
        }
    }
}
