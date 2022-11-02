using FluentValidation;
using SchoolAPI.Entities;

namespace SchoolAPI.Models.Validators
{
    public class RegisterTeacherDTOValidator : AbstractValidator<RegisterTeacherDTO>
    {

        public RegisterTeacherDTOValidator(SchoolDbContext dbContext)
        {
            RuleFor(t => t.Email).NotEmpty().EmailAddress();
            RuleFor(t => t.Password).MinimumLength(6);
            RuleFor(t => t.ConfirmPassword).Equal(e=>e.Password);

            RuleFor(t => t.Email)
                .Custom((value, context) =>
                {
                   var emailOccupied =  dbContext.Teachers.Any(t=>t.Email == value);

                    if (emailOccupied)
                    {
                        context.AddFailure("Email","Email already exists.");
                    }
                });
        } 
    }
}
