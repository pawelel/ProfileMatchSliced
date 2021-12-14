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
            optionsBuilder.UseSqlServer(@"Server=tcp:profilematchsql.database.windows.net,1433;Initial Catalog=profilematchSQL;Persist Security Info=False;User ID=wsbstudent;Password=PMMaslo123$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
            builder.Entity<UserCategory>().HasKey(x => new { x.ApplicationUserId, x.CategoryId });
            builder.Entity<UserNote>().HasKey(x => new { x.ApplicationUserId, x.NoteId });
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
            builder.Entity<Note>().HasData(
               new Note()
               {
                   Id = 1,
                   Name = "Co jest dla mnie ważne w pracy?"
               },
                    new Note()
                    {
                        Id = 2,
                        Name = "Co jest ważne dla mnie osobiście?"
                    },
                    new Note()
                    {
                        Id = 3,
                        Name = "Moje hobby"
                    },
                    new Note()
                    {
                        Id = 4,
                        Name = "Moje inne umiejętności"
                    },
                    new Note()
                    {
                        Id = 5,
                        Name = "Moje zainteresowania"
                    },
                    new Note()
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
            builder.Entity<Question>().HasData(
                new Question()
                {
                    Id = 1,
                    Name = "C#",
                    CategoryId = 1,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość programowania w C#?"

                },
                new Question()
                {
                    Id = 2,
                    Name = "C++",
                    CategoryId = 1,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość programowania w C++?"

                },
                new Question()
                {
                    Id = 3,
                    Name = "Python",
                    CategoryId = 1,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość programowania w Pythonie?"

                },
                new Question()
                {
                    Id = 4,
                    Name = "Konfiguracja routera",
                    CategoryId = 2,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość sieci komputerowych?"
                },
                new Question()
                {
                Id = 5,
                    Name = "Usługa Active Directory",
                    CategoryId = 2,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość usługi Active Directory?"
                },
                new Question()
                {
                    Id = 6,
                    Name = "Hardware",
                    CategoryId = 3,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość Hardware komputera?"
                },
                new Question()
                {
                    Id = 7,
                    Name = "Instalacja systemu Windows",
                    CategoryId = 3,
                    IsActive = true,
                    Description = "Jaka jest Twoja znajomość na temat instalacji systemu Windows?"
                },
                new Question()
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
                    QuestionId = 1,
                    Level = 1,
                    Description = "Nie znasz podstaw tego języka programowania"
                },
                new AnswerOption()
                {
                    Id = 2,
                    QuestionId = 1,
                    Level = 2,
                    Description = "Znasz podstawowe rzeczy związane z programowaniem w C#"
                },
                new AnswerOption()
                {
                    Id = 3,
                    QuestionId = 1,
                    Level = 3,
                    Description = "Potrafisz pisać proste kody w języku"

                },
                 new AnswerOption()
                 {
                     Id = 4,
                     QuestionId = 1,
                     Level = 4,
                     Description = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)"

                 },
                  new AnswerOption()
                  {
                      Id = 5,
                      QuestionId = 1,
                      Level = 5,
                      Description = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw"

                  },
                new AnswerOption()
                {
                    Id = 6,
                    QuestionId = 2,
                    Level = 1,
                    Description = "Nie znasz podstaw tego języka programowania"
                },
                 new AnswerOption()
                 {
                     Id = 7,
                     QuestionId = 2,
                     Level = 2,
                     Description = "Znasz podstawowe rzeczy związane z programowaniem w C++"
                 },
                new AnswerOption()
                {
                    Id = 8,
                    QuestionId = 2,
                    Level = 3,
                    Description = "Potrafisz pisać proste kody w języku"
                },
                new AnswerOption()
                {
                    Id = 9,
                    QuestionId = 2,
                    Level = 4,
                    Description = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)"

                },
                 new AnswerOption()
                 {
                     Id = 10,
                     QuestionId = 2,
                     Level = 5,
                     Description = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw"

                 },
                 new AnswerOption()
                 {
                     Id = 11,
                     QuestionId = 3,
                     Level = 1,
                     Description = "Nie znasz podstaw tego języka programowania"
                 },
                 new AnswerOption()
                 {
                     Id = 12,
                     QuestionId = 3,
                     Level = 2,
                     Description = "Znasz podstawowe rzeczy związane z programowaniem w Pythonie"
                 },
                new AnswerOption()
                {
                    Id = 13,
                    QuestionId = 3,
                    Level = 3,
                    Description = "Potrafisz pisać proste kody w języku"
                },
                new AnswerOption()
                {
                    Id = 14,
                    QuestionId = 3,
                    Level = 4,
                    Description = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)"

                },
                 new AnswerOption()
                 {
                     Id = 15,
                     QuestionId = 3,
                     Level = 5,
                     Description = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw"

                 },
                 new AnswerOption()
                 {
                     Id = 16,
                     QuestionId = 4,
                     Level = 1,
                     Description = "Znasz podstawowe informacje na temat routera"

                 },
                 new AnswerOption()
                 {
                     Id = 17,
                     QuestionId = 4,
                     Level = 2,
                     Description = "Potrafisz zalogować się do routera i swobodnie poruszasz się po interfejsie"

                 },
                 new AnswerOption()
                 {
                     Id = 18,
                     QuestionId = 4,
                     Level = 3,
                     Description = "Potrafisz skonfigurować podstawowe ustawienia sieciowe w routerze"

                 },
                 new AnswerOption()
                 {
                     Id = 19,
                     QuestionId = 4,
                     Level = 4,
                     Description = "Potrafisz skonfigurować router dla wielu urządzeń oraz zadbać o bezpieczeństwo w sieci"

                 },
                 new AnswerOption()
                 {
                     Id = 20,
                     QuestionId = 4,
                     Level = 5,
                     Description = "Potrafisz skonfigurować router w systemie linux w trybie tekstowym"

                 },
                 new AnswerOption()
                 {
                     Id = 21,
                     QuestionId = 5,
                     Level = 1,
                     Description = "Nie konfigurowałeś żadnej usługi Active Directory"

                 },
                 new AnswerOption()
                 {
                     Id = 22,
                     QuestionId = 5,
                     Level = 2,
                     Description = "Instalowałeś usługę Active Directory, ale jej nie konfigurowałeś"

                 },
                 new AnswerOption()
                 {
                     Id = 23,
                     QuestionId = 5,
                     Level = 3,
                     Description = "Potrafisz dodawać podstawowe usługi do domeny i zrobić prostą konfiguracje"

                 },
                 new AnswerOption()
                 {
                     Id = 24,
                     QuestionId = 5,
                     Level = 4,
                     Description = "Łatwość sprawia ci surfowanie po ustawieniach sieciowych domeny, bez problemu radzisz sobie z tworzeniem domen i dodawaniem kont użytkowników lub grup"

                 },
                 new AnswerOption()
                 {
                     Id = 25,
                     QuestionId = 5,
                     Level = 5,
                     Description = "Usługa AD jest dla ciebie chlebem powszednim i nie sprawia ci żadnych problemów"

                 }
                );
            builder.Entity<UserAnswer>().HasData(
                new UserAnswer()
                {

                    QuestionId = 1,
                    ApplicationUserId = "a96d7c75-47f4-409b-a4d1-03f93c105647",
                    AnswerOptionId = 2

                },
                new UserAnswer()
                {

                    QuestionId = 2,
                    ApplicationUserId = "a96d7c75-47f4-409b-a4d1-03f93c105647",
                    AnswerOptionId = 4

                },
                new UserAnswer()
                {

                    QuestionId = 3,
                    ApplicationUserId = "a96d7c75-47f4-409b-a4d1-03f93c105647",
                    AnswerOptionId = 3

                }
                );
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserCategory> UserNeedCategories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<UserNote> UserNotes { get; set; }
    }
}