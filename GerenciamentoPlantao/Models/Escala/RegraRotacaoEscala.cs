namespace GerenciamentoPlantao.Models.Escala
{
    public class RegraRotacaoEscala : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int OrdemExecucao { get; set; }
        public Departamento Departamento { get; set; } = null!;
        public int DepartamentoId { get; set; }
        public ICollection<RegraRotacaoHorarioEscala> Horarios { get; set; } = new List<RegraRotacaoHorarioEscala>();

        public RegraRotacaoEscala(string nome, int ordemExecucao, int departamentoId, string usuarioId)
        {
            Nome = nome;
            OrdemExecucao = ordemExecucao;
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }

        public RegraRotacaoEscala() { }

        public void Atualizar(string nome, int ordemExecucao, string usuarioId)
        {
            Nome = nome;
            OrdemExecucao = ordemExecucao;
            RegistrarAtualizacao(usuarioId);
        }

        public void AddRegraRotacaoHorarioEscala(RegraRotacaoHorarioEscala horario)
        {
            Horarios.Add(horario);
        }

    }
}
