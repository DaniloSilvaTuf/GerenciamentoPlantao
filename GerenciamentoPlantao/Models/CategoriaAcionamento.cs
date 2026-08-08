namespace GerenciamentoPlantao.Models
{
    public class CategoriaAcionamento : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        public CategoriaAcionamento(string nome, int departamentoId, string usuarioId)
        {
            Nome = nome;
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }

        public void Atualizar(string nome, int departamentoId, string usuarioId)
        {
            Nome = nome;
            DepartamentoId = departamentoId;
            RegistrarAtualizacao(usuarioId);
        }

        public CategoriaAcionamento() { }

        public void Inativar(string usuarioId)
        {
            RegistrarInativacao(usuarioId);
        }

        public void Ativar(string usuarioId)
        {
            RegistrarAtivacao(usuarioId);
        }
    }
}
