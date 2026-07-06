namespace GerenciamentoPlantao.Models
{
    public class Setor
    {
        public int Id { get; private set; }
        public string Nome { get;  set; } = string.Empty;
        public bool Ativo { get;  set; }
        public int EstabelecimentoId { get;  set; }
        public Estabelecimento Estabelecimento { get;  set; } = null;


        protected Setor() { }

        internal Setor(string nome, int estabelecimentoId)
        {
            Nome = nome;
            Ativo = true;
            EstabelecimentoId = estabelecimentoId;
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
