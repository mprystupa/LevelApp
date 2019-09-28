using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LevelApp.DAL.Entities
{
    public partial class LevelAppContext : DbContext
    {
        public LevelAppContext()
        {
        }

        public LevelAppContext(DbContextOptions<LevelAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Schemaversions> Schemaversions { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=levelapp_dev");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schemaversions>(entity =>
            {
                entity.HasKey(e => e.Schemaversionid)
                    .HasName("PRIMARY");

                entity.ToTable("schemaversions");

                entity.Property(e => e.Schemaversionid)
                    .HasColumnName("schemaversionid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Applied)
                    .HasColumnName("applied")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Scriptname)
                    .IsRequired()
                    .HasColumnName("scriptname")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(30)");
            });
        }
    }
}
