namespace GerenciamentoPlantao.Models.Escala
{
    public class DataEspecialEscala : EntidadeBase
    {
        public int Id { get; set; }
        public DateOnly Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Departamento Departamento { get; set; } = null!;
        public int DepartamentoId { get; set; }


        public DataEspecialEscala(string descricao, DateOnly data, int departamentoId, string usuarioId)
        {
            Data = data;
            Descricao = descricao.Trim();
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }

        public DataEspecialEscala() { }

        public void Atualizar(DateOnly data, string descricao, string usuarioId)
        {
            Data = data;
            Descricao = descricao.Trim();
            RegistrarAtualizacao(usuarioId);
        }
    }
}
