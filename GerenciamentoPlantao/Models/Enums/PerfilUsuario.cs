using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.Enums
{
    public enum PerfilUsuario : int
    {
        [Display(Name = "Administrador")]
        Administrador = 0,
        [Display(Name = "Plantonista")]
        Plantonista = 1,
        [Display(Name = "Consulta")]    
        Consulta = 2
    }
}
