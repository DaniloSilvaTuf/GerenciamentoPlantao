using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class CategoriaFormViewModel
    {
        public string Nome { get; set; }
        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; }
    }
}
