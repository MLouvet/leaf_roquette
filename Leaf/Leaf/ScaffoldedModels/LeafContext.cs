using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Leaf.ScaffoldedModels
{
    public partial class LeafContext : DbContext
    {
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Collaborateurs> Collaborateurs { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Projet> Projet { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SuperAdmin> SuperAdmin { get; set; }
        public virtual DbSet<Tache> Tache { get; set; }

        public LeafContext(DbContextOptions<LeafContext> options)
    : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Leaf;Trusted_Connection=True;");
            }
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
                    .HasConstraintName("FK_Client_proj");

                entity.HasOne(d => d.ResponsableNavigation)
                    .WithMany(p => p.Projet)
                    .HasForeignKey(d => d.Responsable)
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
                entity.Property(e => e.ChargeEstimee).HasColumnName("charge_estimee");

                entity.Property(e => e.Debut)
                    .HasColumnName("debut")
                    .HasColumnType("date");

                entity.Property(e => e.Depends).HasColumnName("depends");

                entity.Property(e => e.Fin)
                    .HasColumnName("fin")
                    .HasColumnType("date");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom ")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Progres).HasColumnName("progres");

                entity.Property(e => e.SuperTache).HasColumnName("Super_tache");

                entity.HasOne(d => d.Collab)
                    .WithMany(p => p.Tache)
                    .HasForeignKey(d => d.CollabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Tach_Coll");

                entity.HasOne(d => d.DependsNavigation)
                    .WithMany(p => p.InverseDependsNavigation)
                    .HasForeignKey(d => d.Depends)
                    .HasConstraintName("Fk_Dependance");

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
