
using Microsoft.EntityFrameworkCore;
using GerenciamentoPlantao.Models;
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
        }
    }
}
