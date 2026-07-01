namespace GerenciamentoPlantao.Models
{
    public class Solucao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public Solucao(string nome)
        {
            Nome = nome;
            Ativo = true;
        }

        public void InativarSolucao()
        {
            Ativo = false;
        }

        public void AtivarSolucao()
        {
            Ativo = true;
        }
    }
}
