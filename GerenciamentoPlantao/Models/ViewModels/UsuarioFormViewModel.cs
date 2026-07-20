using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class UsuarioFormViewModel
    {
        public string DescNome { get; set; } = string.Empty;
        public string NmUsuario { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool Plantonista { get; set; }
        public IEnumerable<SelectListItem>? Departamentos { get; set; }
    }
}
