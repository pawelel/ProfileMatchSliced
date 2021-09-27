using FluentValidation;

using ProfileMatch.Models.ViewModels;

namespace ProfileMatch.Validations
{
    public class EditUserValidator : AbstractValidator<ApplicationUserVM>
    {
        public EditUserValidator()
        {
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required.");
        }
    }
}
