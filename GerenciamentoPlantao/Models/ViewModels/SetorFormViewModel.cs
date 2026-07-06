using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class SetorFormViewModel
    {
        public string Nome { get; set; }

        public int EstabelecimentoId { get; set; }

        public IEnumerable<SelectListItem> Estabelecimentos { get; set; }
    }
}
