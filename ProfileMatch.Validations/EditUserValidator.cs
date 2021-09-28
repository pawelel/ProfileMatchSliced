using FluentValidation;



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
