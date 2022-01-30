using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Repositories
{


    public interface IUnitOfWork
    {
        IRepository<Department> Departments { get; }
        IRepository<AnswerOption> AnswerOptions { get; }
        IRepository<ApplicationUser> ApplicationUsers { get; }
        IRepository<Category> Categories { get; }
        IRepository<Certificate> Certificates { get; }
        IRepository<ClosedQuestion> ClosedQuestions { get; }
        IRepository<Job> Jobs { get; }
        IRepository<OpenQuestion> OpenQuestions { get; }
        IRepository<UserCategory> UserCategories { get; }
        IRepository<UserClosedAnswer> UserClosedAnswers { get; }
        IRepository<UserOpenAnswer> UserOpenAnswers { get; }
        IRepository<IdentityRole> IdentityRoles { get; }
        IRepository<IdentityUserRole<string>> IdentityUserRoles { get; }


    }



    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;
      

        public UnitOfWork(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public IRepository<AnswerOption> AnswerOptions => _answerOptions ??= new Repository<AnswerOption, ApplicationDbContext>(_context);
        private IRepository<AnswerOption> _answerOptions;

        public IRepository<ApplicationUser> ApplicationUsers => _users ??= new Repository<ApplicationUser, ApplicationDbContext>(_context);
        private IRepository<ApplicationUser> _users;

        public IRepository<Category> Categories => _categories ??= new Repository<Category, ApplicationDbContext>(_context);
        private IRepository<Category> _categories;

        public IRepository<Certificate> Certificates => _certificates ??= new Repository<Certificate, ApplicationDbContext>(_context);
        private IRepository<Certificate> _certificates;

        public IRepository<ClosedQuestion> ClosedQuestions => _closedQuestions ??= new Repository<ClosedQuestion, ApplicationDbContext>(_context);
        private IRepository<ClosedQuestion> _closedQuestions;

        public IRepository<Department> Departments => _departments ??= new Repository<Department, ApplicationDbContext>(_context);
        private IRepository<Department> _departments;

        public IRepository<IdentityRole> IdentityRoles => _identityRoles ??= new Repository<IdentityRole, ApplicationDbContext>(_context);
        private IRepository<IdentityRole> _identityRoles;

        public IRepository<IdentityUserRole<string>> IdentityUserRoles => _identityUserRoles ??= new Repository<IdentityUserRole<string>, ApplicationDbContext>(_context);
        private IRepository<IdentityUserRole<string>> _identityUserRoles;

        public IRepository<Job> Jobs => _jobs ??= new Repository<Job, ApplicationDbContext>(_context);
        private IRepository<Job> _jobs;

        public IRepository<OpenQuestion> OpenQuestions => _openQuestions ??= new Repository<OpenQuestion, ApplicationDbContext>(_context);
        private IRepository<OpenQuestion> _openQuestions;

        public IRepository<UserCategory> UserCategories => _userCategories ??= new Repository<UserCategory, ApplicationDbContext>(_context);
        private IRepository<UserCategory> _userCategories;

        public IRepository<UserClosedAnswer> UserClosedAnswers => _userClosedAnswers ??= new Repository<UserClosedAnswer, ApplicationDbContext>(_context);
        private IRepository<UserClosedAnswer> _userClosedAnswers;


        public IRepository<UserOpenAnswer> UserOpenAnswers => _userOpenAnswers ??= new Repository<UserOpenAnswer, ApplicationDbContext>(_context);
        private IRepository<UserOpenAnswer> _userOpenAnswers;

    }
}