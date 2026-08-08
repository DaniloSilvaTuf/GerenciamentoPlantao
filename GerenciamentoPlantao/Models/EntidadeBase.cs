namespace GerenciamentoPlantao.Models
{
    public abstract class EntidadeBase
    {
        public bool Ativo { get; set; }

        public DateTime DataInsert { get; set; }
        public string? UsuarioInsertId { get; set; }
        public Usuario? UsuarioInsert { get; set; }

        public DateTime? DataUpdate { get; set; }
        public string? UsuarioUpdateId { get; set; }
        public Usuario? UsuarioUpdate { get; set; }

        public DateTime? DataInativacao { get; set; }
        public string? UsuarioInativacaoId { get; set; }
        public Usuario? UsuarioInativacao { get; set; }

        protected void RegistrarCriacao(string usuarioId)
        {
            Ativo = true;
            DataInsert = DateTime.Now;
            UsuarioInsertId = usuarioId;
        }

        protected void RegistrarAtualizacao(string usuarioId)
        {
            DataUpdate = DateTime.Now;
            UsuarioUpdateId = usuarioId;
        }

        protected void RegistrarInativacao(string usuarioId)
        {
            Ativo = false;
            DataInativacao = DateTime.Now;
            UsuarioInativacaoId = usuarioId;
        }

        protected void RegistrarAtivacao(string usuarioId)
        {
            Ativo = true;
            DataInativacao = null;
            UsuarioInativacaoId = null;
            DataUpdate = DateTime.Now;
            UsuarioUpdateId = usuarioId;
        }
    }
}
