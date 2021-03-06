﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Leaf.DAL.ScaffoldedModels
{
    public partial class LeafContext : DbContext
    {
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Collaborateurs> Collaborateurs { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<PreviousTasks> PreviousTasks { get; set; }
        public virtual DbSet<Projet> Projet { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SuperAdmin> SuperAdmin { get; set; }
        public virtual DbSet<Tache> Tache { get; set; }

        private static IConfiguration _config = null;
        public LeafContext(DbContextOptions<LeafContext> options)
            : base(options)
        {
        }

        public static void SetConfiguration(IConfiguration configuration)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("LeafDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_admin_id");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Adresse)
                    .IsRequired()
                    .HasColumnName("adresse")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Compagnie)
                    .IsRequired()
                    .HasColumnName("compagnie")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasColumnName("telephone")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Collaborateurs>(entity =>
            {
                entity.Property(e => e.Identifiant)
                    .IsRequired()
                    .HasColumnName("identifiant")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mdp)
                    .IsRequired()
                    .HasColumnName("mdp")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasColumnName("prenom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Statut)
                    .IsRequired()
                    .HasColumnName("statut")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.StatutNavigation)
                    .WithMany(p => p.Collaborateurs)
                    .HasForeignKey(d => d.Statut)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_status_collab");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Destinataire).HasColumnName("destinataire");

                entity.Property(e => e.Horodatage)
                    .HasColumnName("horodatage")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdProjet).HasColumnName("idProjet");

                entity.Property(e => e.IdTache).HasColumnName("idTache");

                entity.Property(e => e.Lue).HasColumnName("lue");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .IsUnicode(false);

                entity.HasOne(d => d.DestinataireNavigation)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.Destinataire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_notification_to");

                entity.HasOne(d => d.IdProjetNavigation)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.IdProjet)
                    .HasConstraintName("Fk_projet_id");

                entity.HasOne(d => d.IdTacheNavigation)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.IdTache)
                    .HasConstraintName("Fk_tache_id");
            });

            modelBuilder.Entity<PreviousTasks>(entity =>
            {
                entity.HasOne(d => d.PreviousTaskNavigation)
                    .WithMany(p => p.PreviousTasksPreviousTaskNavigation)
                    .HasForeignKey(d => d.PreviousTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_PreviousTask");

                entity.HasOne(d => d.TaskNavigation)
                    .WithMany(p => p.PreviousTasksTaskNavigation)
                    .HasForeignKey(d => d.Task)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Task");
            });

            modelBuilder.Entity<Projet>(entity =>
            {
                entity.Property(e => e.Client).HasColumnName("client");

                entity.Property(e => e.Debut)
                    .HasColumnName("debut")
                    .HasColumnType("date");

                entity.Property(e => e.Echeance)
                    .HasColumnName("echeance")
                    .HasColumnType("date");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Responsable).HasColumnName("responsable");

                entity.HasOne(d => d.ClientNavigation)
                    .WithMany(p => p.Projet)
                    .HasForeignKey(d => d.Client)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_proj");

                entity.HasOne(d => d.ResponsableNavigation)
                    .WithMany(p => p.Projet)
                    .HasForeignKey(d => d.Responsable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Repo_Proj");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.Nom);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<SuperAdmin>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.SuperAdmin)
                    .HasForeignKey<SuperAdmin>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuperAdmin_id");
            });

            modelBuilder.Entity<Tache>(entity =>
            {
                entity.Property(e => e.ChargeConsommee).HasColumnName("charge_consommee");

                entity.Property(e => e.ChargeEstimee).HasColumnName("charge_estimee");

                entity.Property(e => e.ChargeEstimeeRestante).HasColumnName("charge_estimee_restante");

                entity.Property(e => e.Debut)
                    .HasColumnName("debut")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Fin)
                    .HasColumnName("fin")
                    .HasColumnType("date");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Progres).HasColumnName("progres");

                entity.Property(e => e.SuperTache).HasColumnName("Super_tache");

                entity.HasOne(d => d.Collab)
                    .WithMany(p => p.Tache)
                    .HasForeignKey(d => d.CollabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Tach_Coll");

                entity.HasOne(d => d.IdProjNavigation)
                    .WithMany(p => p.Tache)
                    .HasForeignKey(d => d.IdProj)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Tach_Proj");

                entity.HasOne(d => d.SuperTacheNavigation)
                    .WithMany(p => p.InverseSuperTacheNavigation)
                    .HasForeignKey(d => d.SuperTache)
                    .HasConstraintName("Fk_Sous_Tache");
            });
        }
    }
}
