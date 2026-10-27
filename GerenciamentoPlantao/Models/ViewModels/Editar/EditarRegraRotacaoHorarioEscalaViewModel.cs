namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public class EditarRegraRotacaoHorarioEscalaViewModel : EditarViewModelBase
    {
        public int Id { get; set; }
        public int RegraRotacaoId { get; set; }
        public int BlocoHorarioPostoEscalaId { get; set; }
        public int OrdemRotacao { get; set; }
    }
}
