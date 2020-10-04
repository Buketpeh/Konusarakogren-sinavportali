using Microsoft.EntityFrameworkCore;
using sinavkonusarakogrenn.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sinavkonusarakogrenn.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(250).IsRequired(true);

            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired(true);


            modelBuilder.Entity<Post>().ToTable("Post");

            modelBuilder.Entity<Post>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Post>().Property(p => p.Title).HasMaxLength(500).IsRequired(true);

            modelBuilder.Entity<Post>().Property(p => p.Description).HasMaxLength(1000).IsRequired(true);

            modelBuilder.Entity<Post>().Property(p => p.CreatedDate).IsRequired(true);


            modelBuilder.Entity<Question>().ToTable("Question");

            modelBuilder.Entity<Question>().Property(q => q.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Question>().Property(q => q.Description).HasMaxLength(1000).IsRequired(true);

            modelBuilder.Entity<Question>().Property(q => q.A).HasMaxLength(1250).IsRequired(true);

            modelBuilder.Entity<Question>().Property(q => q.B).HasMaxLength(1250).IsRequired(true);

            modelBuilder.Entity<Question>().Property(q => q.C).HasMaxLength(1250).IsRequired(true);

            modelBuilder.Entity<Question>().Property(q => q.D).HasMaxLength(1250).IsRequired(true);

            modelBuilder.Entity<Question>().Property(q => q.Answer).HasMaxLength(1).IsRequired(true);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Question)
                .WithOne(x => x.Post);
        }
    }
}
