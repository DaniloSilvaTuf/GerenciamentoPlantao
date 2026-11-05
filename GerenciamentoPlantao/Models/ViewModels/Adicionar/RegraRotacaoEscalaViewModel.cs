using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class RegraRotacaoEscalaViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public int OrdemExecucao { get; set; }
        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
