﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfileMatch.Data;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "10a3d590-a0d8-4959-8db8-fca04b5632fd",
                            ConcurrencyStamp = "aa3b47c5-602d-48f0-86a2-c72db5530198",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "d759a464-69e4-4b8d-a817-631073c8b560",
                            ConcurrencyStamp = "1c8f2f55-22a1-4b81-8ab3-4e487dfa8f86",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "560b6a16-a6bc-4050-a21c-8f7ceb49cfa7",
                            ConcurrencyStamp = "b4f9f9e9-6677-4cab-892e-5c8be5a4eff5",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.AnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswerOptions");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JobTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "unassigned"
                        });
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.UserAnswer", b =>
                {
                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AnswerOptionId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("SupervisorId")
                        .HasColumnType("TEXT");

                    b.HasKey("ApplicationUserId", "QuestionId");

                    b.HasIndex("AnswerOptionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.UserCategory", b =>
                {
                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Want")
                        .HasColumnType("INTEGER");

                    b.HasKey("ApplicationUserId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("UserNeedCategories");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.UserNote", b =>
                {
                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("NoteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("ApplicationUserId", "NoteId");

                    b.HasIndex("NoteId");

                    b.ToTable("UserNotes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileMatch.Models.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.AnswerOption", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.Question", "Question")
                        .WithMany("AnswerOptions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.ApplicationUser", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.Department", "Department")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Question", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.Category", "Category")
                        .WithMany("Questions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.UserAnswer", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.AnswerOption", "AnswerOption")
                        .WithMany("UserAnswers")
                        .HasForeignKey("AnswerOptionId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("ProfileMatch.Models.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("UserAnswers")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileMatch.Models.Models.Question", "Question")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnswerOption");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.UserCategory", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("UserNeedCategories")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileMatch.Models.Models.Category", "Category")
                        .WithMany("UserNeedCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.UserNote", b =>
                {
                    b.HasOne("ProfileMatch.Models.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileMatch.Models.Models.Note", "Note")
                        .WithMany("UserNotes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Note");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.AnswerOption", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.ApplicationUser", b =>
                {
                    b.Navigation("UserAnswers");

                    b.Navigation("UserNeedCategories");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Category", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("UserNeedCategories");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Department", b =>
                {
                    b.Navigation("ApplicationUsers");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Note", b =>
                {
                    b.Navigation("UserNotes");
                });

            modelBuilder.Entity("ProfileMatch.Models.Models.Question", b =>
                {
                    b.Navigation("AnswerOptions");

                    b.Navigation("UserAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
