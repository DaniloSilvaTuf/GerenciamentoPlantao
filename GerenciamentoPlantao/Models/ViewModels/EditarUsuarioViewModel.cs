using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class EditarUsuarioViewModel
    {
        public int Id { get; set; }
        public string DescNome { get; set; } = string.Empty;
        public string NmUsuario { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool Plantonista { get; set; }
        public bool Ativo { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; }
    }
}
