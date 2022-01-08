using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Models.Models;

using System;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace ProfileMatch.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=aspnet-ProfileMatchNewest;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //relations
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

            //composite keys
            builder.Entity<UserCategory>().HasKey(x => new { x.ApplicationUserId, x.CategoryId });
            builder.Entity<UserOpenAnswer>().HasKey(x => new { x.ApplicationUserId, x.NoteId });
            //seed roles
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = "93877c5b-c988-4c83-b152-d0b17858f7c6", ConcurrencyStamp = "dd434162-84c9-4f3f-acd0-aa028df9b1f4" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = "8c916fc5-5d08-4164-8594-7ac0e2b6e16a", ConcurrencyStamp = "83256a0f-8959-4eb8-a15e-e9c74c782841" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Manager", NormalizedName = "MANAGER", Id = "af138749-2fc8-4bcf-8492-fadb9e0d5415", ConcurrencyStamp = "6d68df77-faee-4dab-bb84-4c445d4cc7a1" });

            builder.Entity<Department>().HasData(new Department { Id = 1, Name = "unassigned" });
            builder.Entity<Department>().HasData(new Department { Id = 2, Name = "IT" });
            builder.Entity<Department>().HasData(new Department { Id = 3, Name = "HR" });
            base.OnModelCreating(builder);

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>()
        .HasMany(e => e.UserRoles)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
            //Seeding the User to AspNetUsers table
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "a96d7c75-47f4-409b-a4d1-03f93c105647", // primary key
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    PasswordHash = hasher.HashPassword(null, "Maslo123$"),
                    DepartmentId = 1,
                    EmailConfirmed = true,
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    FirstName = "Klark",
                    LastName = "Kent"
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "8c916fc5-5d08-4164-8594-7ac0e2b6e16a",
                    UserId = "a96d7c75-47f4-409b-a4d1-03f93c105647"
                }
            );

            //Seeding first set of questions
            builder.Entity<OpenQuestion>().HasData(
               new OpenQuestion()
               {
                   Id = 1,
                   Name = "Co jest dla mnie ważne w pracy?"
               },
                    new OpenQuestion()
                    {
                        Id = 2,
                        Name = "Co jest ważne dla mnie osobiście?"
                    },
                    new OpenQuestion()
                    {
                        Id = 3,
                        Name = "Moje hobby"
                    },
                    new OpenQuestion()
                    {
                        Id = 4,
                        Name = "Moje inne umiejętności"
                    },
                    new OpenQuestion()
                    {
                        Id = 5,
                        Name = "Moje zainteresowania"
                    },
                    new OpenQuestion()
                    {
                        Id = 6,
                        Name = "Jakie są moje cele?"
                    }
                );
            builder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Programowanie"
                },
                new Category()
                {
                    Name = "Sieci komputerowe",
                    Id = 2
                },
                new Category()
                {
                    Name = "Obsługa komputera",
                    Id = 3
                },
                new Category()
                {
                    Name = "Handel",
                    Id = 4
                },
                 new Category()
                 {
                     Name = "Lingwistyka",
                     Id = 5
                 }

                );
            builder.Entity<ClosedQuestion>().HasData(
                new ClosedQuestion()
                {
                    Id = 1,
                    Name = "C#",
                    CategoryId = 1,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość programowania w C#?"

                },
                new ClosedQuestion()
                {
                    Id = 2,
                    Name = "C++",
                    CategoryId = 1,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość programowania w C++?"

                },
                new ClosedQuestion()
                {
                    Id = 3,
                    Name = "Python",
                    CategoryId = 1,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość programowania w Pythonie?"

                },
                new ClosedQuestion()
                {
                    Id = 4,
                    Name = "Konfiguracja routera",
                    CategoryId = 2,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość sieci komputerowych?"
                },
                new ClosedQuestion()
                {
                Id = 5,
                    Name = "Usługa Active Directory",
                    CategoryId = 2,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość usługi Active Directory?"
                },
                new ClosedQuestion()
                {
                    Id = 6,
                    Name = "Hardware",
                    CategoryId = 3,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość Hardware komputera?"
                },
                new ClosedQuestion()
                {
                    Id = 7,
                    Name = "Instalacja systemu Windows",
                    CategoryId = 3,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość na temat instalacji systemu Windows?"
                },
                new ClosedQuestion()
                {
                    Id = 8,
                    Name = "Obsługa programu magazynowego",
                    CategoryId = 4,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość obsługi programów magazynowych?"
                }
                );
            builder.Entity<AnswerOption>().HasData(
                new AnswerOption()
                {
                    Id = 1,
                    ClosedQuestionId = 1,
                    Level = 1,
                    Description = "Nie znasz podstaw tego języka programowania"
                },
                new AnswerOption()
                {
                    Id = 2,
                    ClosedQuestionId = 1,
                    Level = 2,
                    Description = "Znasz podstawowe rzeczy związane z programowaniem w C#"
                },
                new AnswerOption()
                {
                    Id = 3,
                    ClosedQuestionId = 1,
                    Level = 3,
                    Description = "Potrafisz pisać proste kody w języku"

                },
                 new AnswerOption()
                 {
                     Id = 4,
                     ClosedQuestionId = 1,
                     Level = 4,
                     Description = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)"

                 },
                  new AnswerOption()
                  {
                      Id = 5,
                      ClosedQuestionId = 1,
                      Level = 5,
                      Description = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw"

                  },
                new AnswerOption()
                {
                    Id = 6,
                    ClosedQuestionId = 2,
                    Level = 1,
                    Description = "Nie znasz podstaw tego języka programowania"
                },
                 new AnswerOption()
                 {
                     Id = 7,
                     ClosedQuestionId = 2,
                     Level = 2,
                     Description = "Znasz podstawowe rzeczy związane z programowaniem w C++"
                 },
                new AnswerOption()
                {
                    Id = 8,
                    ClosedQuestionId = 2,
                    Level = 3,
                    Description = "Potrafisz pisać proste kody w języku"
                },
                new AnswerOption()
                {
                    Id = 9,
                    ClosedQuestionId = 2,
                    Level = 4,
                    Description = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)"

                },
                 new AnswerOption()
                 {
                     Id = 10,
                     ClosedQuestionId = 2,
                     Level = 5,
                     Description = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw"

                 },
                 new AnswerOption()
                 {
                     Id = 11,
                     ClosedQuestionId = 3,
                     Level = 1,
                     Description = "Nie znasz podstaw tego języka programowania"
                 },
                 new AnswerOption()
                 {
                     Id = 12,
                     ClosedQuestionId = 3,
                     Level = 2,
                     Description = "Znasz podstawowe rzeczy związane z programowaniem w Pythonie"
                 },
                new AnswerOption()
                {
                    Id = 13,
                    ClosedQuestionId = 3,
                    Level = 3,
                    Description = "Potrafisz pisać proste kody w języku"
                },
                new AnswerOption()
                {
                    Id = 14,
                    ClosedQuestionId = 3,
                    Level = 4,
                    Description = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)"

                },
                 new AnswerOption()
                 {
                     Id = 15,
                     ClosedQuestionId = 3,
                     Level = 5,
                     Description = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw"

                 },
                 new AnswerOption()
                 {
                     Id = 16,
                     ClosedQuestionId = 4,
                     Level = 1,
                     Description = "Znasz podstawowe informacje na temat routera"

                 },
                 new AnswerOption()
                 {
                     Id = 17,
                     ClosedQuestionId = 4,
                     Level = 2,
                     Description = "Potrafisz zalogować się do routera i swobodnie poruszasz się po interfejsie"

                 },
                 new AnswerOption()
                 {
                     Id = 18,
                     ClosedQuestionId = 4,
                     Level = 3,
                     Description = "Potrafisz skonfigurować podstawowe ustawienia sieciowe w routerze"

                 },
                 new AnswerOption()
                 {
                     Id = 19,
                     ClosedQuestionId = 4,
                     Level = 4,
                     Description = "Potrafisz skonfigurować router dla wielu urządzeń oraz zadbać o bezpieczeństwo w sieci"

                 },
                 new AnswerOption()
                 {
                     Id = 20,
                     ClosedQuestionId = 4,
                     Level = 5,
                     Description = "Potrafisz skonfigurować router w systemie linux w trybie tekstowym"

                 },
                 new AnswerOption()
                 {
                     Id = 21,
                     ClosedQuestionId = 5,
                     Level = 1,
                     Description = "Nie konfigurowałeś żadnej usługi Active Directory"

                 },
                 new AnswerOption()
                 {
                     Id = 22,
                     ClosedQuestionId = 5,
                     Level = 2,
                     Description = "Instalowałeś usługę Active Directory, ale jej nie konfigurowałeś"

                 },
                 new AnswerOption()
                 {
                     Id = 23,
                     ClosedQuestionId = 5,
                     Level = 3,
                     Description = "Potrafisz dodawać podstawowe usługi do domeny i zrobić prostą konfiguracje"

                 },
                 new AnswerOption()
                 {
                     Id = 24,
                     ClosedQuestionId = 5,
                     Level = 4,
                     Description = "Łatwość sprawia ci surfowanie po ustawieniach sieciowych domeny, bez problemu radzisz sobie z tworzeniem domen i dodawaniem kont użytkowników lub grup"

                 },
                 new AnswerOption()
                 {
                     Id = 25,
                     ClosedQuestionId = 5,
                     Level = 5,
                     Description = "Usługa AD jest dla ciebie chlebem powszednim i nie sprawia ci żadnych problemów"

                 }
                );
            builder.Entity<UserClosedAnswer>().HasData(
                new UserClosedAnswer()
                {

                    ClosedQuestionId = 1,
                    ApplicationUserId = "a96d7c75-47f4-409b-a4d1-03f93c105647",
                    AnswerOptionId = 2

                },
                new UserClosedAnswer()
                {

                    ClosedQuestionId = 2,
                    ApplicationUserId = "a96d7c75-47f4-409b-a4d1-03f93c105647",
                    AnswerOptionId = 4

                },
                new UserClosedAnswer()
                {

                    ClosedQuestionId = 3,
                    ApplicationUserId = "a96d7c75-47f4-409b-a4d1-03f93c105647",
                    AnswerOptionId = 3

                }
                );
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClosedQuestion> ClosedQuestions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<UserClosedAnswer> UserClosedAnswers { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<OpenQuestion> OpenQuestions { get; set; }
        public DbSet<UserOpenAnswer> UserOpenAnswers { get; set; }
    }
}