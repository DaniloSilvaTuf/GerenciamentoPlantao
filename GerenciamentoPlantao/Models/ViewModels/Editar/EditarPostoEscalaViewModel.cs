using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarPostoEscalaViewModel : EditarViewModelBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public int DepartamentoId { get; set; }
        public string DepartamentoNome { get; set; } = null!;
    }
}
