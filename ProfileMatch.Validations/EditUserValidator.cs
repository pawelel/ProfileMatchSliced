using FluentValidation;

using ProfileMatch.Models.ViewModels;

using System;

namespace ProfileMatch.Validations
{
    public class EditUserValidator : AbstractValidator<EditUserModel>
    {
        public EditUserValidator()
        {
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required.");
        }
    }
}
