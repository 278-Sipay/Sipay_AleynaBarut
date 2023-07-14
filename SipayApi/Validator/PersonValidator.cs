using FluentValidation;
using SipayApi.Models;

namespace SipayApi.Validator
{
    public class PersonValidator:AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Staff person name is required.")
               .Length(5, 100).WithMessage("The staff person's name should have a character count ranging from 5 to 100 characters.");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Staff person lastname is required.")
                .Length(5, 100).WithMessage("The staff person's lastname should have a character count ranging from 5 to 100 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Staff person phone number is required.")
                .Matches("^\\d{10}$").WithMessage("Staff person phone number should contain only numerical digits.");

            RuleFor(x => x.AccessLevel)
                .NotEmpty().WithMessage("Staff person access level is required.")
                .InclusiveBetween(1, 5).WithMessage("The access level of the staff person to the system should be within the range of 1 to 5.");

            RuleFor(x => x.Salary)
                .NotEmpty().WithMessage("Salary level is required.")
                .InclusiveBetween(5000, 50000).WithMessage("Staff person salary must be between 5000 and 50000.")
            .Custom((salary, context) =>
            {
                var person = (Person)context.InstanceToValidate;
                switch (person.AccessLevel)
                {
                    case 1:
                        if (salary > 10000)
                            context.AddFailure("Salary cannot be greater than 10000 for Access Level 1.");
                        break;
                    case 2:
                        if (salary > 20000)
                            context.AddFailure("Salary cannot be greater than 20000 for Access Level 2.");
                        break;
                    case 3:
                        if (salary > 30000)
                            context.AddFailure("Salary cannot be greater than 30000 for Access Level 3.");
                        break;
                    case 4:
                        if (salary > 40000)
                            context.AddFailure("Salary cannot be greater than 40000 for Access Level 4.");
                        break;
                    default:
                        context.AddFailure("Invalid Access Level.");
                        break;
                }
            });

        }
    }
}
