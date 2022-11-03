using Microsoft.AspNetCore.Authorization;
using SchoolAPI.Entities;
using System.Security.Claims;

namespace SchoolAPI.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, School>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ResourceOperationRequirement requirement, School school)
        {
            if(requirement.Operation == ResourceOperation.Read ||
                requirement.Operation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if(school.CreatedById == int.Parse(userId)){
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
