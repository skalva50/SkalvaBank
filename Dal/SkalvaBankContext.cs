using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SkalvaBank.Domain;

namespace SkalvaBank.Dal
{
    public partial class SkalvaBankContext : DbContext
    {
        public SkalvaBankContext()
        {
        }

        public SkalvaBankContext(DbContextOptions<SkalvaBankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssLibCategorie> AssLibCategorie { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Typecategorie> Typecategorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=SkalvaBank;Username=lecornu;Password=N2h*idEV3Aq0INXkxhl4");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssLibCategorie>(entity =>
            {
                entity.ToTable("ass_lib_categorie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCategorie).HasColumnName("id_categorie");

                entity.Property(e => e.Libelle)
                    .HasColumnName("libelle")
                    .HasColumnType("character varying(255)");

                entity.HasOne(d => d.IdCategorieNavigation)
                    .WithMany(p => p.AssLibCategorie)
                    .HasForeignKey(d => d.IdCategorie)
                    .HasConstraintName("ass_lib_categorie_id_categorie_fkey");
            });

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.ToTable("categorie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HorsStats).HasColumnName("hors_stats");

                entity.Property(e => e.IdTypecategorie).HasColumnName("id_typecategorie");

                entity.Property(e => e.Libelle)
                    .HasColumnName("libelle")
                    .HasColumnType("character varying(500)");

                entity.HasOne(d => d.IdTypecategorieNavigation)
                    .WithMany(p => p.Categorie)
                    .HasForeignKey(d => d.IdTypecategorie)
                    .HasConstraintName("categorie_id_typecategorie");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.ToTable("operation");

                entity.HasIndex(e => new { e.Dateoperation, e.Libelle, e.Reference, e.Montant, e.Numcompte })
                    .HasName("unique_constraint_operation")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dateoperation).HasColumnName("dateoperation");

                entity.Property(e => e.IdCategorie).HasColumnName("id_categorie");

                entity.Property(e => e.Libelle)
                    .HasColumnName("libelle")
                    .HasColumnType("character varying(500)");

                entity.Property(e => e.Montant).HasColumnName("montant");

                entity.Property(e => e.Numcompte)
                    .HasColumnName("numcompte")
                    .HasColumnType("character varying(16)");

                entity.Property(e => e.Reference)
                    .HasColumnName("reference")
                    .HasColumnType("character varying(7)");

                entity.Property(e => e.Sens).HasColumnName("sens");

                entity.HasOne(d => d.IdCategorieNavigation)
                    .WithMany(p => p.Operation)
                    .HasForeignKey(d => d.IdCategorie)
                    .HasConstraintName("operation_id_categorie_fkey");
            });

            modelBuilder.Entity<Typecategorie>(entity =>
            {
                entity.ToTable("typecategorie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Libelle)
                    .HasColumnName("libelle")
                    .HasColumnType("character varying");
            });
        }
    }
}
