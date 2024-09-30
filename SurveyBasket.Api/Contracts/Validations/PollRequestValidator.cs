using FluentValidation;

namespace SurveyBasket.Api.Contracts.Validations;

public class PollRequestValidator :AbstractValidator<PollRequest>
{
    public PollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .Length(3, 100)
            .WithMessage("{PropertyName} must be between 3 to 100 char");
        RuleFor(x => x.Summary)
            .NotEmpty()
            .Length(18,1500)
            .WithMessage("{PropertyName} must be between 18 to 1500 char ");

        RuleFor(x=>x.StartAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(x => x.EndAt)
            .NotEmpty();
            
        RuleFor(x => x)
            .Must(HasValidDate)
            .WithName(nameof(PollRequest.EndAt))
            .WithMessage("{PropertyName} must be greater than or equals start date");

            

    }

    private  bool HasValidDate(PollRequest request) => request.EndAt >= request.StartAt;





}
