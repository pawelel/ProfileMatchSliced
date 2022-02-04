using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Models.Entities;

using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace ProfileMatch.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //composite keys
            builder.Entity<UserCategory>().HasKey(x => new { x.ApplicationUserId, x.CategoryId });
            builder.Entity<UserOpenAnswer>().HasKey(x => new { x.ApplicationUserId, x.OpenQuestionId });
            //seed roles
            
            
            base.OnModelCreating(builder);
            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<Category>().HasMany(q=>q.ClosedQuestions)
                .WithOne(q => q.Category)
                .HasForeignKey(q => q.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>(user =>
            {
                user.HasMany(e => e.UserRoles)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
                user.HasOne(j => j.Job)
                .WithMany(a => a.ApplicationUsers)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.ClientCascade);
                user.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            });

            builder.Entity<UserClosedAnswer>(entity =>
            {
                entity.HasKey(x => new { x.ApplicationUserId, x.ClosedQuestionId });
                entity.HasOne(a => a.AnswerOption)
                .WithMany(u => u.UserClosedAnswers)
                .HasForeignKey(a => a.AnswerOptionId)
                .OnDelete(DeleteBehavior.ClientCascade);
                entity.HasOne(q => q.ClosedQuestion)
                .WithMany(a => a.UserClosedAnswers)
                .HasForeignKey(q => q.ClosedQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(u => u.ApplicationUser)
                .WithMany(a => a.UserClosedAnswers)
                .HasForeignKey(u => u.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            });
            builder.PopulateSeeds();
        }


        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClosedQuestion> ClosedQuestions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<UserClosedAnswer> UserClosedAnswers { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<OpenQuestion> OpenQuestions { get; set; }
        public DbSet<UserOpenAnswer> UserOpenAnswers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

    }
}