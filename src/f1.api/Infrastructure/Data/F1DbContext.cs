using F1.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace F1.Api.Infrastructure.Data;

public class F1DbContext : DbContext
{
    public F1DbContext(DbContextOptions<F1DbContext> options) : base(options)
    {
    }

    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<Piloto> Pilotos { get; set; }
    public DbSet<Corrida> Corridas { get; set; }
    public DbSet<Resultado> Resultados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Equipe>(entity =>
        {
            entity.HasKey(e => e.EquipeId);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Pais).IsRequired().HasMaxLength(50);
            entity.Property(e => e.AnoFundacao);
            entity.HasMany(e => e.Pilotos)
                .WithOne(p => p.Equipe)
                .HasForeignKey(p => p.EquipeId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Piloto>(entity =>
        {
            entity.HasKey(p => p.PilotoId);
            entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Nacionalidade).IsRequired().HasMaxLength(50);
            entity.Property(p => p.DataNascimento);
        });

        modelBuilder.Entity<Corrida>(entity =>
        {
            entity.HasKey(c => c.CorridaId);
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Local).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Data).IsRequired();
        });

        modelBuilder.Entity<Resultado>(entity =>
        {
            entity.HasKey(r => r.ResultadoId);
            
            entity.HasOne(r => r.Piloto)
                .WithMany(p => p.Resultados)
                .HasForeignKey(r => r.PilotoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Corrida)
                .WithMany(c => c.Resultados)
                .HasForeignKey(r => r.CorridaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(r => r.Posicao).IsRequired();
            entity.Property(r => r.Pontos).IsRequired();
        });

        modelBuilder.Entity<Equipe>().HasData(
            new { EquipeId = 1, Nome = "Red Bull Racing", Pais = "Áustria", AnoFundacao = (int?)2005 },
            new { EquipeId = 2, Nome = "Mercedes-AMG Petronas", Pais = "Alemanha", AnoFundacao = (int?)2010 },
            new { EquipeId = 3, Nome = "Scuderia Ferrari", Pais = "Itália", AnoFundacao = (int?)1950 }
        );
    }
}

