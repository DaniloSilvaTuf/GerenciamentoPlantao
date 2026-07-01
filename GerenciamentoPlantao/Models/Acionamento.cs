using Microsoft.AspNetCore.Components.Web;

namespace GerenciamentoPlantao.Models
{
    public class Acionamento
    {
        public int Id { get; set; }
        public DateTime DataAcionamento { get; set; }
        public DateTime DataRegistro { get; set; }
        public Canal Canal { get; set; }
        public int CanalId { get; set; }
        public Usuario Plantonista { get; set; }
        public int UsuarioId { get; set; }
        public string? Acionador { get; set; }
        public int? NrAtendimento { get; set; }
        public Estabelecimento Estabelecimento { get; set; }
        public int EstabelecimentoId { get; set; }
        public Setor Setor { get; set; }
        public int SetorId { get; set; }
        public CategoriaAcionamento CategoriaAcionamento { get; set; }
        public int CategoriaAcionamentoId { get; set; }
        public bool Apoio { get; set; }
        public string? DescProblema { get; set; }
        public Solucao Solucao { get; set; }
        public int SolucaoId { get; set; }
        public string? Observacao { get; set; }

        public Acionamento(DateTime dataAcionamento, DateTime dataRegistro, int canalId, int usuarioId, int estabelecimentoId, int setorId, int categoriaAcionamentoId, bool apoio, int solucaoId)
        {
            DataAcionamento = dataAcionamento;
            DataRegistro = DateTime.Now;
            CanalId = canalId;
            UsuarioId = usuarioId;
            EstabelecimentoId = estabelecimentoId;
            SetorId = setorId;
            CategoriaAcionamentoId = categoriaAcionamentoId;
            Apoio = apoio;
            SolucaoId = solucaoId;
        }

        public void AtualizarAcionamento(DateTime dataAcionamento, int canalId, int usuarioId, int estabelecimentoId, int setorId, int categoriaAcionamentoId, bool apoio, int solucaoId)
        {
            DataAcionamento = dataAcionamento;
            CanalId = canalId;
            UsuarioId = usuarioId;
            EstabelecimentoId = estabelecimentoId;
            SetorId = setorId;
            CategoriaAcionamentoId = categoriaAcionamentoId;
            Apoio = apoio;
            SolucaoId = solucaoId;
        }
    }
}
