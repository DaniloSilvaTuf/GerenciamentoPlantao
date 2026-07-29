using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Informe o usuário")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
