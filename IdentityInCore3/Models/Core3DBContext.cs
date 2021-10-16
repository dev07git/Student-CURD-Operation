using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IdentityInCore3.DAL.Models
{
    public partial class Core3DBContext : DbContext
    {
        public Core3DBContext()
        {
        }

        public Core3DBContext(DbContextOptions<Core3DBContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<SubjectMaster> SubjectMaster { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //                optionsBuilder.UseSqlServer("Server=serverName;Database=;User ID=;Password=;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<SubjectMaster>(entity =>
            {

                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                //entity.Property(e => e.Id).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                //entity.Property(e => e.Id).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.PostalCode).HasMaxLength(10);
            });


            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                //entity.Property(e => e.Id).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.HasIndex(e => e.SubjectMasterId);
                entity.HasOne(d => d.SubjectMaster)
          .WithMany(p => p.Subjects)
          .HasForeignKey(d => d.SubjectMasterId)
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("FK_Subjects_SubjectMaster");


                entity.HasIndex(e => e.StudentId);
                entity.HasOne(d => d.Student)
          .WithMany(p => p.Subjects)
          .HasForeignKey(d => d.StudentId)
          .OnDelete(DeleteBehavior.Cascade)
          .HasConstraintName("FK_Subjects_Students");

            });

            modelBuilder.Entity<SubjectMaster>()
                .HasData(
                new SubjectMaster() { Id=1, Name="Math", CreatedOn=DateTimeOffset.UtcNow},
                new SubjectMaster() { Id=2,Name = "Hindi", CreatedOn = DateTimeOffset.UtcNow },
                new SubjectMaster() { Id=3,Name = "EVS", CreatedOn = DateTimeOffset.UtcNow },
                new SubjectMaster() { Id=4,Name = "English", CreatedOn = DateTimeOffset.UtcNow }

                );
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
