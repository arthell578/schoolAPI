using FluentValidation;

namespace SchoolAPI.Models.Validators
{
    public class SchoolQueryValidator : AbstractValidator<SchoolQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15, 20 };
        public SchoolQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Page Size must be [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
