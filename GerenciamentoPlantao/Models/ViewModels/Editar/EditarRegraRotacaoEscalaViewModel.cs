namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarRegraRotacaoEscalaViewModel : EditarViewModelBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int OrdemExecucao { get; set; }
        public int DepartamentoId { get; set; }
    }
}
