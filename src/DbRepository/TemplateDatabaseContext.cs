using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DbRepository.Models;
using DbRepository.Seeds;

namespace DbRepository
{
    public partial class TemplateDatabaseContext : DbContext
    {
        internal TemplateDatabaseContext() { }
        public TemplateDatabaseContext(DbContextOptions<TemplateDatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Data Source=\"localhost, 1111\";Initial Catalog=TemplateDb;Persist Security Info=True;User ID=sa;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .ValueGeneratedNever();

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasColumnName("age");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("getdate()")
                    .HasColumnType("datetime");

            });
            OnModelCreatingPartial(modelBuilder);
            Seed(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedUsers();
        }


    }
}


