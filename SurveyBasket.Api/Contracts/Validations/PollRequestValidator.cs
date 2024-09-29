namespace SurveyBasket.Api.Contracts.Validations;

public class PollRequestValidator :AbstractValidator<PollRequest>
{
    public PollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .Length(3, 100)
            .WithMessage("{PropertyName} must be between 3 to 100 char");
        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(50,1500)
            .WithMessage("{PropertyName} must be between 50 to 1500 char ");
      
    }

}
