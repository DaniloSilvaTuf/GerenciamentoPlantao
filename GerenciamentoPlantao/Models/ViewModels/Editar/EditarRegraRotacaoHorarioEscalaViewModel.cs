namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarRegraRotacaoHorarioEscalaViewModel : EditarViewModelBase
    {
        public int Id { get; set; }
        public int RegraRotacaoId { get; set; }
        public int BlocoHorarioEscalaId { get; set; }
        public int OrdemRotacao { get; set; }
    }
}
