namespace GerenciamentoPlantao.Models
{
    public class Estabelecimento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Setor> Setores { get; set; } = new List<Setor>();

        public Estabelecimento(string nome)
        {
            Nome = nome;
            Ativo = true;
        }

        public void AddSetor(Setor setor)
        {
            if (Setores.Any(s => s.Nome == setor.Nome))
            {
                throw new InvalidOperationException("Setor com o mesmo nome já existe nesse Estabelecimento.");
            }
            Setores.Add(setor);
        }

        public void InativarSetor(Setor setor)
        {
            if (!Setores.Contains(setor))
            {
                throw new InvalidOperationException("Setor não pertence a esse Estabelecimento.");
            }

            setor.Ativo = false;
        }

        public void AtivarSetor(Setor setor)
        {
            if (!Setores.Contains(setor))
            {
                throw new InvalidOperationException("Setor não pertence a esse Estabelecimento.");
            }

            setor.Ativo = true;
        }
    }
}
