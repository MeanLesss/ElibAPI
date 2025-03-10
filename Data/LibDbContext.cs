using System;
using System.Collections.Generic;
using ElibAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ElibAPI.Data
{
    public partial class LibDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public LibDbContext()
        {
        }

        public LibDbContext(DbContextOptions<LibDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Download> Downloads { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<TeachersGroup> TeachersGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Ensure configuration is available
                var config = _configuration ?? new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                string connectionString = config.GetConnectionString("LibDbContext") ?? config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.GroupId).HasColumnName("group_id");
                entity.Property(e => e.Path).HasColumnName("path");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Download>(entity =>
            {
                entity.ToTable("downloads");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("groups");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<TeachersGroup>(entity =>
            {
                entity.ToTable("teachers_groups");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.GroupId).HasColumnName("group_id");
                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "IX_users_username").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ForwardAddr).HasColumnName("forward_addr");
                entity.Property(e => e.GroupId).HasColumnName("group_id");
                entity.Property(e => e.Image).HasColumnName("image");
                entity.Property(e => e.Pwd).HasColumnName("pwd");
                entity.Property(e => e.RemoteAddr).HasColumnName("remote_addr");
                entity.Property(e => e.Role)
                    .HasDefaultValueSql("'Student'")
                    .HasColumnName("role");
                entity.Property(e => e.Token).HasColumnName("token");
                entity.Property(e => e.Username).HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
