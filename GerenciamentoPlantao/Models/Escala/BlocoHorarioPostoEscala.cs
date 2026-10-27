namespace GerenciamentoPlantao.Models.Escala
{
    public class BlocoHorarioPostoEscala : EntidadeBase
    {
        public int Id { get; set; }
        public BlocoHorarioEscala BlocoHorarioEscala { get; set; } = null!;
        public int BlocoHorarioEscalaId { get; set; }
        public PostoEscala PostoEscala { get; set; } = null!;
        public int PostoEscalaId { get; set; }

        public BlocoHorarioPostoEscala(int blocoHorarioEscalaId, int postoEscalaId, string usuarioId)
        {
            BlocoHorarioEscalaId = blocoHorarioEscalaId;
            PostoEscalaId = postoEscalaId;
            RegistrarCriacao(usuarioId);
        }

        public BlocoHorarioPostoEscala() { }
    }
}
