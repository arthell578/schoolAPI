using Microsoft.AspNetCore.Authorization;

namespace SchoolAPI.Authorization
{
    public class CreatedMin2SchoolsRequirement : IAuthorizationRequirement
    {
        public int MinSchoolsCreated { get; set; }

        public CreatedMin2SchoolsRequirement(int minSchoolsCreated)
        {
            MinSchoolsCreated = minSchoolsCreated;
        }
    }
}
