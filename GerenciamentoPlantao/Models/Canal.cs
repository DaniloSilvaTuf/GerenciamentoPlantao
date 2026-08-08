using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models
{
    public class Canal : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;     
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        public Canal(string nome, int departamentoId, string usuarioId)
        {
            Nome = nome;
            DepartamentoId = departamentoId;
            RegistrarCriacao(usuarioId);
        }
        public Canal() { }

        public void Atualizar(string nome, int departamentoId, string usuarioId)
        {
            Nome = nome;
            DepartamentoId = departamentoId;
            RegistrarAtualizacao(usuarioId);
        }

        public void Inativar(string usuarioId)
        {
            RegistrarInativacao(usuarioId);
        }

        public void Ativar(string usuarioId)
        {
            RegistrarAtivacao(usuarioId);
        }
    }
}
