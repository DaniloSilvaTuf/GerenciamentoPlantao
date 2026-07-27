using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public Usuario(string descNome, string nmUsuario, string email, string telefone, bool plantonista, bool ativo, PerfilUsuario perfil, int departamentoId)
        {
            DescNome = descNome;
            UserName = nmUsuario;
            Email = email;
            PhoneNumber = telefone;
            Plantonista = plantonista;
            Ativo = true;
            Perfil = 0;
            DepartamentoId = departamentoId;
        }

        public Usuario() { }

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
