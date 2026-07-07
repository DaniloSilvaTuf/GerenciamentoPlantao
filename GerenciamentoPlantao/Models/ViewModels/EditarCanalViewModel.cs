using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels
{
    public class EditarCanalViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }
        public int DepartamentoId { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; }
    }
}
