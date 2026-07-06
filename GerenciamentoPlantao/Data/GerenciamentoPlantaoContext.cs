
using Microsoft.EntityFrameworkCore;
using GerenciamentoPlantao.Models;

namespace GerenciamentoPlantao.Data
{
    public class GerenciamentoPlantaoContext : DbContext
    {
        public GerenciamentoPlantaoContext(DbContextOptions<GerenciamentoPlantaoContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Canal> Canais { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<CategoriaAcionamento> CategoriasAcionamento { get; set; }
        public DbSet<Solucao> Solucoes { get; set; }
        public DbSet<Acionamento> Acionamentos { get; set; }

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
                .WithMany()
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
        }
    }
}
