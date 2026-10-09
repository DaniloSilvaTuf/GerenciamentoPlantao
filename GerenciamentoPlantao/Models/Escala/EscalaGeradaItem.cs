using GerenciamentoPlantao.Exceptions;

namespace GerenciamentoPlantao.Models.Escala
{
    public class EscalaGeradaItem : EntidadeBase
    {
        public int Id { get; set; }
        public EscalaGerada EscalaGerada { get; set; } = null!;
        public int EscalaGeradaId { get; set; }
        public PostoEscala PostoEscala { get; set; } = null!;
        public int PostoEscalaId { get; set; }
        public string NomePosto { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = null!;
        public string UsuarioId { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }


        public EscalaGeradaItem(int escalaGeradaId, int postoEscalaId, string nomePosto, string usuarioId, DateTime dataHoraInicio, DateTime dataHoraFim, string usuarioCriacaoId)
        {
            if (dataHoraFim <= dataHoraInicio)
            {
                throw new BusinessException("A data e hora de fim deve ser maior que a data e hora de início.");
            }

            EscalaGeradaId = escalaGeradaId;
            PostoEscalaId = postoEscalaId;
            NomePosto = nomePosto;
            UsuarioId = usuarioId;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            RegistrarCriacao(usuarioCriacaoId);
        }

        public EscalaGeradaItem() { }

        public void AlterarUsuario(string usuarioNovoId, string usuarioAlteracaoId)
        {
            UsuarioId = usuarioNovoId;
            RegistrarAtualizacao(usuarioAlteracaoId);
        }
    }
}
