using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class UsuarioFormViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 200 caracteres.")]
        public string DescNome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter entre 3 e 30 caracteres.")]
        public string NmUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        
        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }

        public IEnumerable<SelectListItem> Perfis { get; set; } = Enumerable.Empty<SelectListItem>();
        public PerfilUsuario Perfil { get; set; }
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; } = string.Empty;

        public bool Plantonista { get; set; }
        public IEnumerable<SelectListItem>? Departamentos { get; set; }
    }
}
