namespace GerenciamentoPlantao.Models
{
    public class Solucao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public Solucao(string nome)
        {
            Nome = nome;
            Ativo = true;
        }

        protected Solucao() { }

        public void Inativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}
