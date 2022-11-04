using Microsoft.AspNetCore.Authorization;
using SchoolAPI.Entities;

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
            throw new NotImplementedException();
        }
    }
}
