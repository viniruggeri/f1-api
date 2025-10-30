using System;
using F1.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace f1.api.Migrations
{
    [DbContext(typeof(F1DbContext))]
    partial class F1DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("F1.Api.Domain.Entities.Corrida", b =>
                {
                    b.Property<int>("CorridaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CorridaId"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.HasKey("CorridaId");

                    b.ToTable("Corridas");
                });

            modelBuilder.Entity("F1.Api.Domain.Entities.Equipe", b =>
                {
                    b.Property<int>("EquipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EquipeId"));

                    b.Property<int?>("AnoFundacao")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.HasKey("EquipeId");

                    b.ToTable("Equipes");

                    b.HasData(
                        new
                        {
                            EquipeId = 1,
                            AnoFundacao = 2005,
                            Nome = "Red Bull Racing",
                            Pais = "Áustria"
                        },
                        new
                        {
                            EquipeId = 2,
                            AnoFundacao = 2010,
                            Nome = "Mercedes-AMG Petronas",
                            Pais = "Alemanha"
                        },
                        new
                        {
                            EquipeId = 3,
                            AnoFundacao = 1950,
                            Nome = "Scuderia Ferrari",
                            Pais = "Itália"
                        });
                });

            modelBuilder.Entity("F1.Api.Domain.Entities.Piloto", b =>
                {
                    b.Property<int>("PilotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PilotoId"));

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("EquipeId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Nacionalidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.HasKey("PilotoId");

                    b.HasIndex("EquipeId");

                    b.ToTable("Pilotos");
                });

            modelBuilder.Entity("F1.Api.Domain.Entities.Resultado", b =>
                {
                    b.Property<int>("ResultadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultadoId"));

                    b.Property<int>("CorridaId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("PilotoId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Pontos")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Posicao")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("ResultadoId");

                    b.HasIndex("CorridaId");

                    b.HasIndex("PilotoId");

                    b.ToTable("Resultados");
                });

            modelBuilder.Entity("F1.Api.Domain.Entities.Piloto", b =>
                {
                    b.HasOne("F1.Api.Domain.Entities.Equipe", "Equipe")
                        .WithMany("Pilotos")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Equipe");
                });

            modelBuilder.Entity("F1.Api.Domain.Entities.Resultado", b =>
                {
                    b.HasOne("F1.Api.Domain.Entities.Corrida", "Corrida")
                        .WithMany("Resultados")
                        .HasForeignKey("CorridaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("F1.Api.Domain.Entities.Piloto", "Piloto")
                        .WithMany("Resultados")
                        .HasForeignKey("PilotoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Corrida");

                    b.Navigation("Piloto");
                });

            modelBuilder.Entity("F1.Api.Domain.Entities.Corrida", b =>
                {
                    b.Navigation("Resultados");
                });

            modelBuilder.Entity("F1.Api.Domain.Entities.Equipe", b =>
                {
                    b.Navigation("Pilotos");
                });

            modelBuilder.Entity("F1.Api.Domain.Entities.Piloto", b =>
                {
                    b.Navigation("Resultados");
                });
#pragma warning restore 612, 618
        }
    }
}
