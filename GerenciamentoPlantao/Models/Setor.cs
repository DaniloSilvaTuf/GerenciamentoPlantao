namespace GerenciamentoPlantao.Models
{
    public class Setor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int EstabelecimentoId { get; set; }
        public Estabelecimento Estabelecimento { get; set; }

        public Setor(string nome, int estabelecimentoId)
        {
            Nome = nome;
            Ativo = true;
            EstabelecimentoId = estabelecimentoId;
        }
    }
}
