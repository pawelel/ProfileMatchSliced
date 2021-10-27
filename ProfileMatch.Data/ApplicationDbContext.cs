using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Models.Models;

using System;

namespace ProfileMatch.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //relations
            builder.Entity<UserAnswer>(entity =>
            {
                entity.HasKey(x => new { x.ApplicationUserId, x.QuestionId });
                entity.HasOne(a => a.AnswerOption)
                .WithMany(u => u.UserAnswers)
                .HasForeignKey(a => a.AnswerOptionId)
                .OnDelete(DeleteBehavior.ClientCascade);
                entity.HasOne(q => q.Question)
                .WithMany(a => a.UserAnswers)
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(u => u.ApplicationUser)
                .WithMany(a => a.UserAnswers)
                .HasForeignKey(u => u.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            //composite keys
            builder.Entity<UserNeedCategory>().HasKey(x => new { x.ApplicationUserId, x.CategoryId });
            builder.Entity<UserNote>().HasKey(x => new { x.ApplicationUserId, x.NoteId });
            //seed roles
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Manager", NormalizedName = "MANAGER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });

            builder.Entity<Department>().HasData(new Department { Id = 1, Name = "unassigned" });
            base.OnModelCreating(builder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserNeedCategory> UserNeedCategories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<UserNote> UserNotes { get; set; }
    }
}