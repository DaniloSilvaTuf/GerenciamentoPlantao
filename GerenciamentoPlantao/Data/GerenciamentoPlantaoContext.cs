
using Microsoft.EntityFrameworkCore;
using GerenciamentoPlantao.Models;
using GerenciamentoPlantao.Models.Escala;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GerenciamentoPlantao.Data
{
    public class GerenciamentoPlantaoContext : IdentityDbContext<Usuario>
    {
        public GerenciamentoPlantaoContext(DbContextOptions<GerenciamentoPlantaoContext> options) : base(options)
        {
        }

        public DbSet<Canal> Canais { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<CategoriaAcionamento> CategoriasAcionamento { get; set; }
        public DbSet<Solucao> Solucoes { get; set; }
        public DbSet<Acionamento> Acionamentos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<EscalaBase> EscalasBase { get; set; }
        public DbSet<EscalaBaseItem> EscalasBaseItems { get; set; }
        public DbSet<EscalaBaseRegraRotacao> EscalasBaseRegrasRotacao { get; set; }
        public DbSet<EscalaGerada> EscalasGeradas { get; set; }
        public DbSet<EscalaGeradaItem> EscalasGeradasItems { get; set; }
        public DbSet<HistoricoAlteracaoEscala> HistoricosAlteracaoEscala { get; set; }
        public DbSet<PostoEscala> PostosEscalas { get; set; }
        public DbSet<RegraRotacaoEscala> RegrasRotacao { get; set; }
        public DbSet<RegraRotacaoHorarioEscala> RegrasRotacaoHorarios { get; set; }
        public DbSet<BlocoHorarioEscala> BlocosHorarioEscala { get; set; }
        public DbSet<BlocoHorarioPostoEscala> BlocosHorarioPostosEscala { get; set; }
        public DbSet<DataEspecialEscala> DatasEspeciaisEscala { get; set; }
        public DbSet<ApoioPlantonistaEscala> ApoiosPlantonistasEscala { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Setor>()
                .HasOne(s => s.Estabelecimento)
                .WithMany(e => e.Setores)
                .HasForeignKey(s => s.EstabelecimentoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.Estabelecimento)
                .WithMany()
                .HasForeignKey(a => a.EstabelecimentoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.Setor)
                .WithMany()
                .HasForeignKey(a => a.SetorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.Canal)
                .WithMany()
                .HasForeignKey(a => a.CanalId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.Plantonista)
                .WithMany(u => u.Acionamentos)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.CategoriaAcionamento)
                .WithMany()
                .HasForeignKey(a => a.CategoriaAcionamentoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.Solucao)
                .WithMany()
                .HasForeignKey(a => a.SolucaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Estabelecimento>()
                .Property(e => e.Ativo)
                .HasDefaultValue(true);

            modelBuilder.Entity<Departamento>()
                .Property(d => d.Ativo)
                .HasDefaultValue(true);

            modelBuilder.Entity<Canal>()
                .HasOne(c => c.Departamento)
                .WithMany(d => d.Canais)
                .HasForeignKey(c => c.DepartamentoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoriaAcionamento>()
                .HasOne(c => c.Departamento)
                .WithMany(d => d.CategoriasAcionamentos)
                .HasForeignKey(c => c.DepartamentoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.Departamento)
                .WithMany(d => d.Solucoes)
                .HasForeignKey(s => s.DepartamentoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Departamento)
                .WithMany(d => d.Usuarios)
                .HasForeignKey(u => u.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            /* Relacionamentos Usuários */
            modelBuilder.Entity<Canal>()
                .HasOne(c => c.UsuarioInsert)
                .WithMany()
                .HasForeignKey(c => c.UsuarioInsertId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Canal>()
                .HasOne(c => c.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(c => c.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Canal>()
                .HasOne(c => c.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(c => c.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoriaAcionamento>()
                .HasOne(c => c.UsuarioInsert)
                .WithMany()
                .HasForeignKey(c => c.UsuarioInsertId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoriaAcionamento>()
                .HasOne(c => c.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(c => c.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoriaAcionamento>()
                .HasOne(c => c.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(c => c.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Departamento>()
                .HasOne(d => d.UsuarioInsert)
                .WithMany()
                .HasForeignKey(d => d.UsuarioInsertId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Departamento>()
                .HasOne(d => d.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(d => d.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Departamento>()
                .HasOne(d => d.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(d => d.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Estabelecimento>()
                .HasOne(e => e.UsuarioInsert)
                .WithMany()
                .HasForeignKey(e => e.UsuarioInsertId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Estabelecimento>()
                .HasOne(e => e.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(e => e.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Estabelecimento>()
                .HasOne(e => e.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(e => e.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.UsuarioInsert)
                .WithMany()
                .HasForeignKey(s => s.UsuarioInsertId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(s => s.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Solucao>()
                .HasOne(s => s.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(s => s.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Setor>()
                .HasOne(s => s.UsuarioInsert)
                .WithMany()
                .HasForeignKey(s => s.UsuarioInsertId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Setor>()
                .HasOne(s => s.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(s => s.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Setor>()
                .HasOne(s => s.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(s => s.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.UsuarioInsert)
                .WithMany()
                .HasForeignKey(a => a.UsuarioInsertId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(a => a.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acionamento>()
                .HasOne(a => a.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(a => a.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.UsuarioInsert)
                .WithMany()
                .HasForeignKey(u => u.UsuarioInsertId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(u => u.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(u => u.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            /* ESCALAS */
            modelBuilder.Entity<ApoioPlantonistaEscala>()
                .HasOne(a => a.ApoioUsuario)
                .WithMany()
                .HasForeignKey(a => a.ApoioUsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApoioPlantonistaEscala>()
                .HasOne(a => a.PlantonistaUsuario)
                .WithMany()
                .HasForeignKey(a => a.PlantonistaUsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoricoAlteracaoEscala>()
                .HasOne(h => h.UsuarioAnterior)
                .WithMany()
                .HasForeignKey(h => h.UsuarioAnteriorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoricoAlteracaoEscala>()
                .HasOne(h => h.NovoUsuario)
                .WithMany()
                .HasForeignKey(h => h.NovoUsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BlocoHorarioPostoEscala>()
                .HasIndex(b => new
                {
                    b.BlocoHorarioEscalaId,
                    b.PostoEscalaId
                })
                .IsUnique();

            modelBuilder.Entity<DataEspecialEscala>()
                .HasIndex(d => new
                {
                    d.DepartamentoId,
                    d.Data
                })
                .IsUnique();

            modelBuilder.Entity<EscalaBaseItem>()
                .HasIndex(e => new
                {
                    e.EscalaBaseId,
                    e.BlocoHorarioPostoEscalaId
                })
                .IsUnique();

            modelBuilder.Entity<EscalaBaseRegraRotacao>()
                .HasIndex(e => new
                {
                    e.EscalaBaseId,
                    e.RegraRotacaoId
                })
                .IsUnique();

            modelBuilder.Entity<RegraRotacaoEscala>()
                .HasIndex(r => new
                {
                    r.DepartamentoId,
                    r.OrdemExecucao
                })
                .IsUnique();

            modelBuilder.Entity<RegraRotacaoHorarioEscala>()
                .HasOne(h => h.RegraRotacao)
                .WithMany(r => r.Horarios)
                .HasForeignKey(h => h.RegraRotacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EscalaGeradaItem>()
                .HasOne(i => i.EscalaGerada)
                .WithMany(e => e.EscalaGeradaItems)
                .HasForeignKey(i => i.EscalaGeradaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HistoricoAlteracaoEscala>()
                .HasOne(h => h.EscalaGeradaItem)
                .WithMany()
                .HasForeignKey(h => h.EscalaGeradaItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EscalaGeradaItem>()
                .HasOne(i => i.PostoEscala)
                .WithMany()
                .HasForeignKey(i => i.PostoEscalaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EscalaBaseItem>()
                .HasOne(i => i.EscalaBase)
                .WithMany(e => e.EscalaBaseItems)
                .HasForeignKey(i => i.EscalaBaseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EscalaBaseRegraRotacao>()
                .HasOne(r => r.EscalaBase)
                .WithMany(e => e.RegrasRotacao)
                .HasForeignKey(r => r.EscalaBaseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EscalaGerada>()
                .HasOne(e => e.EscalaBase)
                .WithMany()
                .HasForeignKey(e => e.EscalaBaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BlocoHorarioPostoEscala>()
                .HasOne(b => b.BlocoHorarioEscala)
                .WithMany()
                .HasForeignKey(b => b.BlocoHorarioEscalaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BlocoHorarioPostoEscala>()
                .HasOne(p => p.PostoEscala)
                .WithMany()
                .HasForeignKey(p => p.PostoEscalaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EscalaBaseItem>()
                .HasOne(i => i.BlocoHorarioPostoEscala)
                .WithMany()
                .HasForeignKey(i => i.BlocoHorarioPostoEscalaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EscalaBaseItem>()
                .HasOne(i => i.Usuario)
                .WithMany()
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EscalaBaseRegraRotacao>()
                .HasOne(r => r.RegraRotacao)
                .WithMany()
                .HasForeignKey(r => r.RegraRotacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EscalaGeradaItem>()
                .HasOne(i => i.Usuario)
                .WithMany()
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostoEscala>()
                .HasOne(p => p.Departamento)
                .WithMany(d => d.PostosEscalas)
                .HasForeignKey(p => p.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BlocoHorarioEscala>()
                .HasOne(b => b.Departamento)
                .WithMany(d => d.BlocosHoarioEscala)
                .HasForeignKey(b => b.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegraRotacaoEscala>()
                .HasOne(r => r.Departamento)
                .WithMany()
                .HasForeignKey(r => r.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DataEspecialEscala>()
                .HasOne(d => d.Departamento)
                .WithMany(dep => dep.DatasEspeciaisEscalas)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApoioPlantonistaEscala>()
                .HasOne(a => a.Departamento)
                .WithMany()
                .HasForeignKey(a => a.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EscalaBase>()
                .HasOne(e => e.Departamento)
                .WithMany()
                .HasForeignKey(e => e.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EscalaGerada>()
                .HasOne(e => e.Departamento)
                .WithMany()
                .HasForeignKey(e => e.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegraRotacaoHorarioEscala>()
                .HasOne(r => r.BlocoHorarioPostoEscala)
                .WithMany()
                .HasForeignKey(r => r.BlocoHorarioPostoEscalaId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<RegraRotacaoHorarioEscala>()
                .HasIndex(r => new
                {
                    r.RegraRotacaoId,
                    r.BlocoHorarioPostoEscalaId
                })
                .IsUnique();

            modelBuilder.Entity<RegraRotacaoHorarioEscala>()
                .HasIndex(r => new
                {
                    r.RegraRotacaoId,
                    r.OrdemRotacao
                })
                .IsUnique();


            /* AUDITORIA ESCALA */
            ConfigurarAuditoria<PostoEscala>(modelBuilder);
            ConfigurarAuditoria<BlocoHorarioEscala>(modelBuilder);
            ConfigurarAuditoria<BlocoHorarioPostoEscala>(modelBuilder);
            ConfigurarAuditoria<RegraRotacaoEscala>(modelBuilder);
            ConfigurarAuditoria<RegraRotacaoHorarioEscala>(modelBuilder);
            ConfigurarAuditoria<DataEspecialEscala>(modelBuilder);
            ConfigurarAuditoria<ApoioPlantonistaEscala>(modelBuilder);
            ConfigurarAuditoria<EscalaBase>(modelBuilder);
            ConfigurarAuditoria<EscalaBaseItem>(modelBuilder);
            ConfigurarAuditoria<EscalaBaseRegraRotacao>(modelBuilder);
            ConfigurarAuditoria<EscalaGerada>(modelBuilder);
            ConfigurarAuditoria<EscalaGeradaItem>(modelBuilder);
            ConfigurarAuditoria<HistoricoAlteracaoEscala>(modelBuilder);
        }

        private static void ConfigurarAuditoria<T>(ModelBuilder modelBuilder)
    where T : EntidadeBase
        {
            modelBuilder.Entity<T>()
                .HasOne(e => e.UsuarioInsert)
                .WithMany()
                .HasForeignKey(e => e.UsuarioInsertId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<T>()
                .HasOne(e => e.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(e => e.UsuarioUpdateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<T>()
                .HasOne(e => e.UsuarioInativacao)
                .WithMany()
                .HasForeignKey(e => e.UsuarioInativacaoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
