using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class EditarSetorViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }
        public int EstabelecimentoId { get; set; }
        public IEnumerable<SelectListItem>  Estabelecimentos { get; set; }
    }

}