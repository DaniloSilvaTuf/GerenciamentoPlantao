namespace GerenciamentoPlantao.Models.Escala
{
    public class RegraRotacao : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Ordem { get; set; }
        public bool Obrigatoria { get; set; }
        public BlocoHorarioPostoEscala BlocoHorarioPostoEscala { get; set; } = null!;
        public int BlocoHorarioPostoEscalaId { get; set; }
        public Departamento Departamento { get; set; } = null!;
        public int DepartamentoId { get; set; }
        public ICollection<RegraRotacaoHorario> Horarios { get; set; } = new List<RegraRotacaoHorario>();

        public RegraRotacao(string nome, int ordem, bool obrigatoria, int blocoHorarioPostoEscalaId, int departamentoId, string usuarioId)
        {
            Nome = nome;
            Ordem = ordem;
            Obrigatoria = obrigatoria;
            BlocoHorarioPostoEscalaId = blocoHorarioPostoEscalaId;
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }

        public RegraRotacao() { }

        public void Ativar(string usuarioId)
        {
            RegistrarAtivacao(usuarioId);
        }

        public void Inativar(string usuarioId)
        {
            RegistrarInativacao(usuarioId);
        }
    }
}
