using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarAcionamentoViewModel : EditarViewModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data do acionamento é obrigatória.")]
        [DataType(DataType.DateTime)]
        public DateTime DataAcionamento { get; set; }
        public string NomePlantonista { get; set; } = string.Empty;
        public string UsuarioId { get; set; }
        public int DepartamentoId { get; set; }
        public int CanalId { get; set; }
        public IEnumerable<SelectListItem> Canal { get; set; } = null!;
    
        public string? Acionador { get; set; }
        public int? NrAtendimento { get; set; }

        [Required(ErrorMessage = "Selecione uma estabelecimento.")]
        public int EstabelecimentoId { get; set; }
        public IEnumerable<SelectListItem> Estabelecimento { get; set; } = null!;

        [Required(ErrorMessage = "Selecione uma setor.")]
        public int SetorId { get; set; }
        public IEnumerable<SelectListItem> Setor { get; set; } = null!;

        [Required(ErrorMessage = "Selecione uma categoria.")]
        public int CategoriaId { get; set; }
        public IEnumerable<SelectListItem> Categoria { get; set; } = null!;

        [Required(ErrorMessage = "Selecione uma solução.")]
        public int SolucaoId { get; set; }
        public IEnumerable<SelectListItem> Solucao { get; set; } = null!;
        
        public bool Apoio { get; set; }
        public string? Observacao { get; set; }
    }
}