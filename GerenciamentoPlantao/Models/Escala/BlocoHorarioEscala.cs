using GerenciamentoPlantao.Models.Enums;

namespace GerenciamentoPlantao.Models.Escala
{
    public class BlocoHorarioEscala : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
        public TipoDiaSemana DiaSemana { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        public BlocoHorarioEscala(string nome, TimeOnly horaInicio, TimeOnly horaFim, TipoDiaSemana diaSemana, int departamentoId, string usuarioId)
        {
            Nome = nome;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            DiaSemana = diaSemana;
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }

        public BlocoHorarioEscala() { }

        public void Atualizar(string novoNome, TimeOnly novaHoraInicio, TimeOnly novaHoraFim, TipoDiaSemana novoDiaSemana, string usuarioId)
        {
            Nome = novoNome;
            HoraInicio = novaHoraInicio;
            HoraFim = novaHoraFim;
            DiaSemana = novoDiaSemana;
            RegistrarAtualizacao(usuarioId);
        }

        public void Ativar(string usuarioId)
        {
            RegistrarAtualizacao(usuarioId);
        }

        public void Inativar(string usuarioId)
        {
            RegistrarAtualizacao(usuarioId);
        }
    }
}
