namespace GerenciamentoPlantao.Models.Escala
{
    public class HistoricoAlteracaoEscala : EntidadeBase
    {
        public int Id { get; set; }
        public EscalaGeradaItem EscalaGeradaItem { get; set; } = null!;
        public int EscalaGeradaItemId { get; set; }
        public Usuario UsuarioAnterior { get; set; } = null!;
        public string UsuarioAnteriorId { get; set; } = string.Empty;
        public Usuario NovoUsuario { get; set; } = null!;
        public string NovoUsuarioId { get; set; } = string.Empty;

        public HistoricoAlteracaoEscala(int escalaGeradaItemId, string usuarioAnteriorId, string novoUsuarioId, string usuarioCriacaoId)
        {
            EscalaGeradaItemId = escalaGeradaItemId;
            UsuarioAnteriorId = usuarioAnteriorId;
            NovoUsuarioId = novoUsuarioId;
            RegistrarCriacao(usuarioCriacaoId);
        }

        public HistoricoAlteracaoEscala() { }
    }
}
