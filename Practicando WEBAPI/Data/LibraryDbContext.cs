using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practicando_WEBAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Data
{
    public class LibraryDbContext: IdentityDbContext
    {
      

        public DbSet<BreedEntity> Breeds { get; set; }
        public DbSet<HeroEntity> Heroes { get; set; }
        public DbSet<SkillEntity> Skills { get; set; }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options):
        base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BreedEntity>().ToTable("Breeds");
            modelBuilder.Entity<BreedEntity>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<BreedEntity>().HasMany(c => c.Heroes).WithOne(v => v.Breed);

            modelBuilder.Entity<HeroEntity>().ToTable("Heroes");
            modelBuilder.Entity<HeroEntity>().Property(v => v.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<HeroEntity>().HasOne(v => v.Breed).WithMany(c => c.Heroes);
            
            modelBuilder.Entity<SkillEntity>().ToTable("Skills");
            modelBuilder.Entity<SkillEntity>().Property(v => v.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<SkillEntity>().HasOne(v => v.Hero).WithMany(c => c.Skills);
        }
    }
}
