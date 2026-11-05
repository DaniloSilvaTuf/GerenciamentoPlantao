namespace GerenciamentoPlantao.Models.Escala
{
    public class EscalaBaseItem : EntidadeBase
    {
        public int Id { get; set; }

        public EscalaBase EscalaBase { get; set; } = null!;
        public int EscalaBaseId { get; set; }

        public BlocoHorarioEscala BlocoHorarioEscala { get; set; } = null!;
        public int BlocoHorarioEscalaId { get; set; }

        public Usuario Usuario { get; set; } = null!;
        public string UsuarioId { get; set; } = string.Empty;

        public EscalaBaseItem(
            int escalaBaseId,
            int blocoHorarioEscalaId,
            string usuarioId,
            string usuarioCriacaoId)
        {
            EscalaBaseId = escalaBaseId;
            BlocoHorarioEscalaId = blocoHorarioEscalaId;
            UsuarioId = usuarioId;

            RegistrarCriacao(usuarioCriacaoId);
        }

        public EscalaBaseItem() { }
    }
}