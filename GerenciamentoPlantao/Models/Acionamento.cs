using Microsoft.AspNetCore.Components.Web;

namespace GerenciamentoPlantao.Models
{
    public class Acionamento : EntidadeBase
    {
        public int Id { get; set; }
        public DateTime DataAcionamento { get; set; }
        public Canal Canal { get; set; } = null!;
        public int CanalId { get; set; }
        public Usuario Plantonista { get; set; } = null!;
        public string UsuarioId { get; set; } = string.Empty;
        public string? Acionador { get; set; }
        public int? NrAtendimento { get; set; }
        public Estabelecimento Estabelecimento { get; set; } = null!;
        public int EstabelecimentoId { get; set; }
        public Setor Setor { get; set; } = null!;
        public int SetorId { get; set; }
        public CategoriaAcionamento CategoriaAcionamento { get; set; } = null!;
        public int CategoriaAcionamentoId { get; set; }
        public bool Apoio { get; set; }
        public string? DescProblema { get; set; }
        public Solucao Solucao { get; set; } = null!;
        public int SolucaoId { get; set; }
        public string? Observacao { get; set; }

        public Acionamento(DateTime dataAcionamento, int canalId, string usuarioId, string? acionador, int? nrAtendimento, int estabelecimentoId, int setorId, int categoriaAcionamentoId, int solucaoId, bool apoio, string? observacao)
        {
            DataAcionamento = dataAcionamento;
            CanalId = canalId;
            UsuarioId = usuarioId;
            Acionador = acionador;
            NrAtendimento = nrAtendimento;
            EstabelecimentoId = estabelecimentoId;
            SetorId = setorId;
            CategoriaAcionamentoId = categoriaAcionamentoId;
            SolucaoId = solucaoId;
            Apoio = apoio;
            Observacao = observacao;
            DataInsert = DateTime.Now;
            UsuarioInsertId = usuarioId;
        }

        public Acionamento() { }

        public void AtualizarAcionamento(DateTime dataAcionamento, int canalId, int estabelecimentoId, int setorId, int categoriaAcionamentoId, bool apoio, int solucaoId, string? observacao, string? acionador, int? nrAtendimento, DateTime dataUpdate, string usuarioUpdateId)
        {
            DataAcionamento = dataAcionamento;
            CanalId = canalId;
            EstabelecimentoId = estabelecimentoId;
            SetorId = setorId;
            CategoriaAcionamentoId = categoriaAcionamentoId;
            Apoio = apoio;
            SolucaoId = solucaoId;
            Observacao = observacao;
            Acionador = acionador;
            NrAtendimento = nrAtendimento;
            DataUpdate = dataUpdate;
            UsuarioUpdateId = usuarioUpdateId;
        }
    }
}
