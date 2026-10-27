namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class RegraRotacaoHorarioEscalaViewModel
    {
        public int RegraRotacaoId { get; set; }
        public int BlocoHorarioPostoEscalaId { get; set; }
        public int OrdemRotacao { get; set; } //Ordem do horário dentro da regra
    }
}
