using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Entities;
using System.Security.Claims;

namespace SchoolAPI.Authorization
{
    public class CreatedMin2SchoolsRequirementHandler : AuthorizationHandler<CreatedMin2SchoolsRequirement>
    {
        private readonly SchoolDbContext _dbContext;

        public CreatedMin2SchoolsRequirementHandler(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMin2SchoolsRequirement requirement)
        {
            var teacherId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var numOfCreatedSchools = _dbContext.Schools.Count(s => s.CreatedById == teacherId);
            
            if(numOfCreatedSchools >= requirement.MinSchoolsCreated)
            {
                context.Succeed(requirement);   
            }

            return Task.CompletedTask;
        }
    }
}
