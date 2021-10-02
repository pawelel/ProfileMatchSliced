using FluentValidation;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Validations
{
    public class EditUserValidator : AbstractValidator<ApplicationUser>
    {
        public EditUserValidator()
        {
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required.");
        }
    }
}