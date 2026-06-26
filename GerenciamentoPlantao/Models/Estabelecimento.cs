namespace GerenciamentoPlantao.Models
{
    public class Estabelecimento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Setor> Setores { get; set; }

        public Estabelecimento(string nome, ICollection<Setor> setores)
        {
            Nome = nome;
            Ativo = true;
            Setores = setores;
        }

        public void AddSetor(Setor setor)
        {
            Setores.Add(setor);
        }

        public void InativarSetor (Setor setor)
        {
            if (!Setores.Contains(setor))
            {
                throw new Exception("Setor não pertence a esse Estabelecimento.");
            }
            
            setor.Ativo = false;
        }
    }
}
