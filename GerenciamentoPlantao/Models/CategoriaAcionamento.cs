namespace GerenciamentoPlantao.Models
{
    public class CategoriaAcionamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public CategoriaAcionamento(string nome, int departamentoId)
        {
            Nome = nome;
            Ativo = true;
            DepartamentoId = departamentoId;
        }

        protected CategoriaAcionamento() { }

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
