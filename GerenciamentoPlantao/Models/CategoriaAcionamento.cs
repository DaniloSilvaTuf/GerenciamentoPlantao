namespace GerenciamentoPlantao.Models
{
    public class CategoriaAcionamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public CategoriaAcionamento(string nome)
        {
            Nome = nome;
            Ativo = true;
        }

        public void InativarCategoria()
        {
            Ativo = false;
        }

        public void AtivarCategoria()
        {
            Ativo = true;
        }
    }
}
