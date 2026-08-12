using GerenciamentoPlantao.Exceptions;

namespace GerenciamentoPlantao.Models
{
    public class Estabelecimento : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Setor> Setores { get; private set; } = new List<Setor>();

        public Estabelecimento(string nome, string usuarioId)
        {
            Nome = nome;
            RegistrarCriacao(usuarioId);
        }

        public void Atualizar(string novoNome, string usuarioId)
        {
            Nome = novoNome;
            RegistrarAtualizacao(usuarioId);
        }

        public Estabelecimento() { }

        public void Ativar(string usuarioId)
        {
            RegistrarAtivacao(usuarioId);
        }

        public void Inativar(string usuarioId)
        {
            RegistrarInativacao(usuarioId);
        }

        public void AddSetor(Setor setor)
        {
            if (Setores.Any(s => s.Nome.Equals(setor.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BusinessException("Já existe um setor com esse nome.");
            }

            Setores.Add(setor);
        }
    }
}
