namespace GerenciamentoPlantao.Models.Escala
{
    public class ApoioPlantonistaEscala : EntidadeBase
    {
        public int Id { get; set; }
        public Usuario ApoioUsuario { get; set; } = null!;
        public string ApoioUsuarioId { get; set; } = string.Empty;
        public Usuario PlantonistaUsuario { get; set; } = null!;
        public string PlantonistaUsuarioId { get; set; } = string.Empty;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
        public Departamento Departamento { get; set; } = null!;
        public int DepartamentoId { get; set; }

        public ApoioPlantonistaEscala(string apoioUsuarioId, string plantonistaUsuarioId, DateOnly dataInicio, DateOnly dataFim, int departamentoId, string usuarioId)
        {
            ApoioUsuarioId = apoioUsuarioId;
            PlantonistaUsuarioId = plantonistaUsuarioId;
            DataInicio = dataInicio;
            DataFim = dataFim;
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }

        public ApoioPlantonistaEscala() { }
    }
}
