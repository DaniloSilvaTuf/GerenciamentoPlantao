using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class UsuarioFormViewModel
    {
        public string DescNome { get; set; } = string.Empty;
        public string NmUsuario { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }       
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }
        public IEnumerable<SelectListItem> Perfis { get; set; } = Enumerable.Empty<SelectListItem>();
        public PerfilUsuario Perfil { get; set; }
        public int DepartamentoId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool Plantonista { get; set; }
        public IEnumerable<SelectListItem>? Departamentos { get; set; }
    }
}
