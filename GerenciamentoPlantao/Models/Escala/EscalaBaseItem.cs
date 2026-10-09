namespace GerenciamentoPlantao.Models.Escala
{
    public class EscalaBaseItem : EntidadeBase
    {
        public int Id { get; set; }
        public EscalaBase EscalaBase { get; set; } = null!;
        public int EscalaBaseId { get; set; }
        public BlocoHorarioPostoEscala BlocoHorarioPostoEscala { get; set; } = null!;
        public int BlocoHorarioPostoEscalaId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public string UsuarioId { get; set; } = string.Empty;

        public EscalaBaseItem(int escalaBaseId, int blocoHorarioPostoEscalaId, string usuarioId, string usuarioCriacaoId)
        {
            EscalaBaseId = escalaBaseId;
            BlocoHorarioPostoEscalaId = blocoHorarioPostoEscalaId;
            UsuarioId = usuarioId;
            RegistrarCriacao(usuarioCriacaoId);
        }

        public EscalaBaseItem() { }
    }
}
