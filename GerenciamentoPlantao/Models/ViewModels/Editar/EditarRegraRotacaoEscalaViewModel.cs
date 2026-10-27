namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarRegraRotacaoEscalaViewModel : EditarViewModelBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int OrdemExecucao { get; set; }
        public Departamento Departamento { get; set; } = null!;
        public int DepartamentoId { get; set; }
    }
}
