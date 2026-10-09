using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPlantao.Models.Enums
{
    public enum StatusAcionamento : int
    {
        [Display(Name = "Resolvido")]
        Resolvido = 1,

        [Display(Name = "Aguardando Apoio")]
        Apoio = 2,

        [Display(Name = "Enviado para Análise")]
        Analise = 3,

    }
}
