using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace GerenciamentoPlantao.Models
{
    public class Usuario : IdentityUser
    {
        public int Id { get; set; }
        public string DescNome { get; set; } = string.Empty;
        public string NmUsuario { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public PerfilUsuario Perfil { get; set; }

        public Usuario(string descNome, string nmUsuario, PerfilUsuario perfil)
        {
            DescNome = descNome;
            NmUsuario = nmUsuario;
            Ativo = true;
            Perfil = perfil;
        }
    }
}
