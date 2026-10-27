namespace GerenciamentoPlantao.Models.ViewModels.Adicionar
{
    public class RegraRotacaoEscalaViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public int OrdemExecucao { get; set; }
        public Departamento Departamento { get; set; } = null!;
        public int DepartamentoId { get; set; }
    }
}
