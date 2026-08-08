namespace GerenciamentoPlantao.Models.ViewModels.Editar
{
    public abstract class EditarViewModelBase
    {
        public DateTime DataInsert { get; set; }
        public string? UsuarioInsertNome { get; set; }
        public DateTime? DataUpdate { get; set; }
        public string? UsuarioUpdateNome { get; set; }
        public DateTime? DataInativacao { get; set; }
        public string? UsuarioInativacaoNome { get; set; }
    }
}
