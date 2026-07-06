namespace GerenciamentoPlantao.Models
{
    public class Estabelecimento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; private set; } = true;
        public ICollection<Setor> Setores { get; private set; } = new List<Setor>();

        public Estabelecimento(string nome)
        {
            Nome = nome;
            Ativo = true;
        }

        public Estabelecimento() { }

        public void AddSetor(Setor setor)
        {
            if (Setores.Any(s => s.Nome.Equals(setor.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Já existe um setor com esse nome.");
            }

            Setores.Add(setor);
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
