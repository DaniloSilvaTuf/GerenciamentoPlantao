namespace GerenciamentoPlantao.Models.Escala
{
    public class EscalaBaseRegraRotacao : EntidadeBase
    {
        public int Id { get; set; }
        public EscalaBase EscalaBase { get; set; } = null!;
        public int EscalaBaseId { get; set; }
        public RegraRotacao RegraRotacao { get; set; } = null!;
        public int RegraRotacaoId { get; set; }

        public EscalaBaseRegraRotacao(int escalaBaseId, int regraRotacaoId, string usuarioCriacaoId)
        {
            EscalaBaseId = escalaBaseId;
            RegraRotacaoId = regraRotacaoId;
            RegistrarCriacao(usuarioCriacaoId);
        }

        public EscalaBaseRegraRotacao() { }
    }
}
