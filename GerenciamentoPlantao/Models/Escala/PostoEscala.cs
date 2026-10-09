namespace GerenciamentoPlantao.Models.Escala
{
    public class PostoEscala : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        public PostoEscala(string nome, int departamentoId, string usuarioId)
        {
            Nome = nome;
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }

        public PostoEscala() { }

        public void Atualizar(string novoNome, string usuarioId)
        {
            Nome = novoNome;
            RegistrarAtualizacao(usuarioId);
        }

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
