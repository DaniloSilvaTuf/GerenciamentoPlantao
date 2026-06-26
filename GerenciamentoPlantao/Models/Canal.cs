namespace GerenciamentoPlantao.Models
{
    public class Canal
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public Canal(string nome)
        {
            Nome = nome;
            Ativo = true;
        }
    }
}
