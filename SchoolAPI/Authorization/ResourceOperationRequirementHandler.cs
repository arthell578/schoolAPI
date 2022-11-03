using Microsoft.AspNetCore.Authorization;
using SchoolAPI.Entities;

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
        }
    }
}
