namespace GerenciamentoPlantao.Models
{
    public class Setor : EntidadeBase
    {
        public int Id { get; private set; }
        public string Nome { get;  set; } = string.Empty;
        public int EstabelecimentoId { get;  set; }
        public Estabelecimento Estabelecimento { get; set; } = null!;

        public Setor(string nome, int estabelecimentoId, string usuarioId)
        {
            Nome = nome;
            EstabelecimentoId = estabelecimentoId;
            RegistrarCriacao(usuarioId);
        }

        public void AtualizarSetor(string nome, int estabelecimentoId, string usuarioId)
        {
            Nome = nome;
            EstabelecimentoId = estabelecimentoId;
            RegistrarAtualizacao(usuarioId);
        }

        public Setor() { }

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
