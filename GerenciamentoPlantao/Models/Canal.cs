namespace GerenciamentoPlantao.Models
{
    public class Canal
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public Canal(string nome)
        {
            Nome = nome;
            Ativo = true;
        }
        protected Canal() { }

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
