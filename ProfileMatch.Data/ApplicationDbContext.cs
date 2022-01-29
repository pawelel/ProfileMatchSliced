using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Models.Models;

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
            builder.Entity<IdentityRole>(role =>
            {
                role.HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = "8c916fc5-5d08-4164-8594-7ac0e2b6e16a", ConcurrencyStamp = "83256a0f-8959-4eb8-a15e-e9c74c782841" });
                role.HasData(new IdentityRole { Name = "Manager", NormalizedName = "MANAGER", Id = "af138749-2fc8-4bcf-8492-fadb9e0d5415", ConcurrencyStamp = "6d68df77-faee-4dab-bb84-4c445d4cc7a1" });
                role.HasData(new IdentityRole() { Name = "User", NormalizedName = "USER", Id = "9588cfdb-8071-49c0-82cf-c51f20d305d2", ConcurrencyStamp = "83e0991b-0ddb-4291-bfe6-f9217019fde5"});
            });
            builder.Entity<Department>(department =>
            {
                department.HasData(new Department { Id = 1, Name = "unassigned", NamePl = "nieprzypisany" });
                department.HasData(new Department { Id = 2, Name = "IT", NamePl = "IT" });
                department.HasData(new Department { Id = 3, Name = "HR", NamePl = "HR" });
            }
                );
            base.OnModelCreating(builder);
            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();
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
                user.HasData(
                new ApplicationUser
                {
                    Id = "a96d7c75-47f4-409b-a4d1-03f93c105647", // primary key
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    PasswordHash = hasher.HashPassword(null, "SuperUser123$"),
                    DepartmentId = 1,
                    EmailConfirmed = true,
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    FirstName = "Klark",
                    LastName = "Kent",
                    JobId=1,
                    IsActive = true,
                    PhotoPath = "/blank-profile.png",
                    DateOfBirth = new DateTime(day:26 , month:1, year:1971)
                }
                            );
            });
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
                   NamePl = "Co jest dla mnie ważne w pracy?",
                   Name = "What is important to me at work?"
               },
                    new OpenQuestion()
                    {
                        Id = 2,
                        NamePl = "Co jest ważne dla mnie osobiście?",
                        Name = "What is important to me personally?"
                    },
                    new OpenQuestion()
                    {
                        Id = 3,
                        NamePl = "Moje hobby",
                        Name = "My hobby"
                    },
                    new OpenQuestion()
                    {
                        Id = 4,
                        NamePl = "Moje inne umiejętności",
                        Name = "My Skills"
                    },
                    new OpenQuestion()
                    {
                        Id = 5,
                        NamePl = "Moje zainteresowania",
                        Name = "My Interests"
                    },
                    new OpenQuestion()
                    {
                        Id = 6,
                        NamePl = "Jakie są moje cele?",
                        Name = "What are my goals?"
                    }
                );
            builder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    NamePl = "Programowanie",
                    Name = "Software development"
                },
                new Category()
                {
                    NamePl = "Sieci komputerowe",
                    Id = 2,
                    Name = "Computer networks"
                },
                new Category()
                {
                    NamePl = "Obsługa komputera",
                    Id = 3,
                    Name = "Computer skills"
                },
                new Category()
                {
                    NamePl = "Handel",
                    Id = 4,
                    Name = "Sales"
                },
                 new Category()
                 {
                     NamePl = "Lingwistyka",
                     Id = 5,
                     Name = "Languages"
                 }
                );
            builder.Entity<Job>().HasData(
                new Job()
                {
                    Id = 1,
                    NamePl = "nie przypisano",
                    Name = "not assigned",
                    Description = "Initial Job Title",
                    DescriptionPl = "Wstępne stanowisko"
                });
            builder.Entity<ClosedQuestion>().HasData(
                new ClosedQuestion()
                {
                    Id = 1,
                    Name = "C#",
                    NamePl = "C#",
                    CategoryId = 1,
                    IsActive = true,
                    DescriptionPl = "Jaka jest Twoja znajomość programowania w C#?",
                    Description = "How good are you in C# development?"
                },
                new ClosedQuestion()
                {
                    Id = 2,
                    Name = "C++",
                    NamePl = "C++",
                    CategoryId = 1,
                    IsActive = true,
                    DescriptionPl = "Jaka jest Twoja znajomość programowania w C++?",
                    Description = "How good are you in C++ development?"
                },
                new ClosedQuestion()
                {
                    Id = 3,
                    Name = "Python",
                    NamePl = "Python",
                    CategoryId = 1,
                    IsActive = true,
                    DescriptionPl = "Jaka jest Twoja znajomość programowania w Pythonie?",
                    Description = "How good are you in Python development?"
                },
                new ClosedQuestion()
                {
                    Id = 4,
                    NamePl = "Konfiguracja routera",
                    Name = "Router configuration",
                    CategoryId = 2,
                    IsActive = true,
                    DescriptionPl = "Jaka jest Twoja znajomość sieci komputerowych?",
                    Description = "How good are you in Computer Networks?"
                },
                new ClosedQuestion()
                {
                    Id = 5,
                    NamePl = "Usługa Active Directory",
                    Name = "Active Directory",
                    CategoryId = 2,
                    IsActive = true,
                    DescriptionPl = "Jaka jest Twoja znajomość usługi Active Directory?",
                    Description = "How good are you in Active Directory?"
                },
                new ClosedQuestion()
                {
                    Id = 6,
                    NamePl = "Sprzęt komputerowy",
                    Name = "Hardware",
                    CategoryId = 3,
                    IsActive = true,
                    DescriptionPl = "Jaka jest Twoja znajomość Hardware komputera?",
                    Description = "What is Your Computer Hardware Knowledge?"
                },
                new ClosedQuestion()
                {
                    Id = 7,
                    NamePl = "Instalacja systemu Windows",
                    Name = "Windows installation",
                    CategoryId = 3,
                    IsActive = false,
                    DescriptionPl = "Jaka jest Twoja znajomość na temat instalacji systemu Windows?",
                    Description = "How well you know MS Windows installation?"
                },
                new ClosedQuestion()
                {
                    Id = 8,
                    NamePl = "Obsługa programu magazynowego",
                    Name = "Handling of the warehouse software",
                    CategoryId = 4,
                    IsActive = false,
                    DescriptionPl = "Jaka jest Twoja znajomość obsługi programów magazynowych?",
                    Description = "How good you are in Warehouse Management Software?"
                }
                );
            builder.Entity<AnswerOption>().HasData(
                new AnswerOption()
                {
                    Id = 1,
                    ClosedQuestionId = 1,
                    Level = 1,
                    DescriptionPl = "Nie znasz podstaw tego języka programowania",
                    Description = "You have no idea about this language"
                },
                new AnswerOption()
                {
                    Id = 2,
                    ClosedQuestionId = 1,
                    Level = 2,
                    DescriptionPl = "Znasz podstawowe pojęcia związane z programowaniem w C#",
                    Description = "You know the basic concepts of C# programming"
                },
                new AnswerOption()
                {
                    Id = 3,
                    ClosedQuestionId = 1,
                    Level = 3,
                    DescriptionPl = "Potrafisz pisać proste kody w języku",
                    Description = "You can write simple C# codes"
                },
                 new AnswerOption()
                 {
                     Id = 4,
                     ClosedQuestionId = 1,
                     Level = 4,
                     DescriptionPl = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)",
                     Description = "You can write code that is more advanced (you know what are conditions, loops, objects, functions)"
                 },
                  new AnswerOption()
                  {
                      Id = 5,
                      ClosedQuestionId = 1,
                      Level = 5,
                      DescriptionPl = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw",
                      Description = "You can easily analyze the code, edit it, introduce new changes or write the program from scratch"
                  },
                new AnswerOption()
                {
                    Id = 6,
                    ClosedQuestionId = 2,
                    Level = 1,
                    DescriptionPl = "Nie znasz podstaw tego języka programowania",
                    Description = "You have no idea about this language"
                },
                 new AnswerOption()
                 {
                     Id = 7,
                     ClosedQuestionId = 2,
                     Level = 2,
                     DescriptionPl = "Znasz podstawowe rzeczy związane z programowaniem w C++",
                     Description = "You know the basic concepts of C++ programming"
                 },
                new AnswerOption()
                {
                    Id = 8,
                    ClosedQuestionId = 2,
                    Level = 3,
                    DescriptionPl = "Potrafisz pisać proste kody w języku",
                    Description = "You can write simple C++ codes"
                },
                new AnswerOption()
                {
                    Id = 9,
                    ClosedQuestionId = 2,
                    Level = 4,
                    DescriptionPl = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)",
                    Description = "You can write code that is more advanced (you know what are conditions, loops, objects, functions)"
                },
                 new AnswerOption()
                 {
                     Id = 10,
                     ClosedQuestionId = 2,
                     Level = 5,
                     DescriptionPl = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw",
                     Description = "You can easily analyze the code, edit it, introduce new changes or write the program from scratch"
                 },
                 new AnswerOption()
                 {
                     Id = 11,
                     ClosedQuestionId = 3,
                     Level = 1,
                     DescriptionPl = "Nie znasz podstaw tego języka programowania",
                     Description = "You have no idea about this language"
                 },
                 new AnswerOption()
                 {
                     Id = 12,
                     ClosedQuestionId = 3,
                     Level = 2,
                     DescriptionPl = "Znasz podstawowe rzeczy związane z programowaniem w Pythonie",
                     Description = "You know the basic concepts of Python programming"
                 },
                new AnswerOption()
                {
                    Id = 13,
                    ClosedQuestionId = 3,
                    Level = 3,
                    DescriptionPl = "Potrafisz pisać proste kody w języku",
                    Description = "You can write simple Python codes"
                },
                new AnswerOption()
                {
                    Id = 14,
                    ClosedQuestionId = 3,
                    Level = 4,
                    DescriptionPl = "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)",
                    Description = "You can write code that is more advanced (you know what are conditions, loops, objects, functions)"
                },
                 new AnswerOption()
                 {
                     Id = 15,
                     ClosedQuestionId = 3,
                     Level = 5,
                     DescriptionPl = "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw",
                     Description = "You can easily analyze the code, edit it, introduce new changes or write the program from scratch"
                 },
                 new AnswerOption()
                 {
                     Id = 16,
                     ClosedQuestionId = 4,
                     Level = 1,
                     DescriptionPl = "Znasz podstawowe informacje na temat routera",
                     Description = "You know the basic information about the router"
                 },
                 new AnswerOption()
                 {
                     Id = 17,
                     ClosedQuestionId = 4,
                     Level = 2,
                     DescriptionPl = "Potrafisz zalogować się do routera i swobodnie poruszasz się po interfejsie",
                     Description = "You can login to the router and you can freely navigate the interface"
                 },
                 new AnswerOption()
                 {
                     Id = 18,
                     ClosedQuestionId = 4,
                     Level = 3,
                     DescriptionPl = "Potrafisz skonfigurować podstawowe ustawienia sieciowe w routerze",
                     Description = "You can configure the basic network settings of the router"
                 },
                 new AnswerOption()
                 {
                     Id = 19,
                     ClosedQuestionId = 4,
                     Level = 4,
                     DescriptionPl = "Potrafisz skonfigurować router dla wielu urządzeń oraz zadbać o bezpieczeństwo w sieci",
                     Description = "You can configure the router for many devices and take care of security in the network"
                 },
                 new AnswerOption()
                 {
                     Id = 20,
                     ClosedQuestionId = 4,
                     Level = 5,
                     DescriptionPl = "Potrafisz skonfigurować router w systemie linux w trybie tekstowym",
                     Description = "Can you configure router in linux system in text mode"
                 },
                 new AnswerOption()
                 {
                     Id = 21,
                     ClosedQuestionId = 5,
                     Level = 1,
                     DescriptionPl = "Nie konfigurowałeś żadnej usługi Active Directory",
                     Description = "You have not configured any Active Directory service"
                 },
                 new AnswerOption()
                 {
                     Id = 22,
                     ClosedQuestionId = 5,
                     Level = 2,
                     DescriptionPl = "Instalowałeś usługę Active Directory, ale jej nie konfigurowałeś",
                     Description = "Youhave installed Active Directory but did not configure it"
                 },
                 new AnswerOption()
                 {
                     Id = 23,
                     ClosedQuestionId = 5,
                     Level = 3,
                     DescriptionPl = "Potrafisz dodawać podstawowe usługi do domeny i zrobić prostą konfigurację",
                     Description = "You can add basic services to the domain and make simple configuration"
                 },
                 new AnswerOption()
                 {
                     Id = 24,
                     ClosedQuestionId = 5,
                     Level = 4,
                     DescriptionPl = "Łatwość sprawia ci surfowanie po ustawieniach sieciowych domeny, bez problemu radzisz sobie z tworzeniem domen i dodawaniem kont użytkowników lub grup",
                     Description = "It's easy for you to surf the domain network settings, you can easily deal with creating domains and adding user or group accounts"
                 },
                 new AnswerOption()
                 {
                     Id = 25,
                     ClosedQuestionId = 5,
                     Level = 5,
                     DescriptionPl = "Usługa AD jest dla ciebie chlebem powszednim i nie sprawia ci żadnych problemów",
                     Description = "AD service is your bread and butter and it doesn't cause you any problems"
                 },
                 new AnswerOption()
                 {
                     Id = 26,
                     ClosedQuestionId = 6,
                     Level = 1,
                     DescriptionPl = "Nigdy nie rozmontowywałeś komputera stacjonarnego lub laptopa",
                     Description = "You have never disassembled your desktop or laptop computer"
                 },
                  new AnswerOption()
                  {
                      Id = 27,
                      ClosedQuestionId = 6,
                      Level = 2,
                      DescriptionPl = "Znasz podstawowe elementy składowe komputera",
                      Description = "You know the basic components of a computer"
                  },
                   new AnswerOption()
                   {
                       Id = 28,
                       ClosedQuestionId = 6,
                       Level = 3,
                       DescriptionPl = "Potrafisz zlokalizować i nazwać dany komponent komputera",
                       Description = "You can locate and name a given computer component"

                   },
                    new AnswerOption()
                    {
                        Id = 29,
                        ClosedQuestionId = 6,
                        Level = 4,
                        DescriptionPl = "Radzisz sobie z montażem podzespołów komputerowych",
                        Description = "You deal with the assembly of computer components"
                    },
                     new AnswerOption()
                     {
                         Id = 30,
                         ClosedQuestionId = 6,
                         Level = 5,
                         DescriptionPl = "Bez problemu składasz od podstaw komputer i go uruchamiasz",
                         Description = "You can easily assemble the computer from scratch and start it",
                     }
                );

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
                entity.HasData(
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
            });
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