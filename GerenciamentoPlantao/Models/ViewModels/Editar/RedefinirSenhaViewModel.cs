using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class RedefinirSenhaViewModel
    {
        public string UsuarioId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a nova senha.")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme a nova senha.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; } = string.Empty;
    }
}
