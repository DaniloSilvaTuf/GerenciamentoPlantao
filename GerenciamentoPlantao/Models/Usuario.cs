using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace GerenciamentoPlantao.Models
{
    public class Usuario : IdentityUser
    {
        public string DescNome { get; set; } = string.Empty;
        public bool Plantonista { get; set; }
        public bool Ativo { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Acionamento> Acionamentos { get; set; } = new List<Acionamento>();
        public DateTime DataInsert { get; set; }
        public string? UsuarioInsertId { get; set; }
        public Usuario? UsuarioInsert { get; set; }
        public DateTime? DataUpdate { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public Usuario? UsuarioUpdate { get; set; }
        public DateTime? DataInativacao { get; set; }
        public string? UsuarioInativacaoId { get; set; }
        public Usuario? UsuarioInativacao { get; set; }

        public Usuario(string descNome, string nmUsuario, string email, string telefone, bool plantonista, PerfilUsuario perfil, int departamentoId, string? usuarioInsertId)
        {
            DescNome = descNome;
            UserName = nmUsuario;
            Email = email;
            PhoneNumber = telefone;
            Plantonista = plantonista;
            Ativo = true;
            Perfil = perfil;
            DepartamentoId = departamentoId;
            UsuarioInsertId = usuarioInsertId;
            DataInsert = DateTime.Now;
        }

        public void Atualizar(string descNome, string email, string telefone, bool plantonista, PerfilUsuario perfil, int departamentoId, string? usuarioUpdateId)
        {
            DescNome = descNome;
            Email = email;
            PhoneNumber = telefone;
            Plantonista = plantonista;
            Perfil = perfil;
            DepartamentoId = departamentoId;
            UsuarioUpdateId = usuarioUpdateId;
            DataUpdate = DateTime.Now;
        }

        public Usuario() { }

        public void Inativar(string usuarioId)
        {
            Ativo = false;
            DataInativacao = DateTime.Now;
            UsuarioInativacaoId = usuarioId;
        }

        public void Ativar(string usuarioId)
        {
            Ativo = true;
            DataUpdate = DateTime.Now;
            UsuarioUpdateId = usuarioId;

            UsuarioInativacaoId = null;
            DataInativacao = null;
        }
    }
}
