using GerenciamentoPlantao.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarUsuarioViewModel : EditarViewModelBase
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 200 caracteres.")]
        public string DescNome { get; set; } = string.Empty;
        
        public string NmUsuario { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; } = string.Empty;
        
        public bool Plantonista { get; set; }
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Selecione um Perfil")]
        public PerfilUsuario Perfil { get; set; }

        public IEnumerable<SelectListItem> Departamentos { get; set; } = null!;

        public bool EhUsuarioLogado { get; set; }
        public bool PodeRedefinirSenha { get; set; }
    }
}
