using Microsoft.AspNetCore.Authorization;

namespace SchoolAPI.Authorization
{
    public class MinimumExeperienceRequiredHandler : AuthorizationHandler<MinimumExperienceRequired>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumExperienceRequired requirement)
        {
            var exp = DateTime.Parse(context.User.FindFirst(c => c.Type == "TeachingStartDate").Value);
            if (exp.AddYears(requirement.MinimumExperience) > DateTime.Now)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
