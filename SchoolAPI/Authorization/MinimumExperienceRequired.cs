using Microsoft.AspNetCore.Authorization;

namespace SchoolAPI.Authorization
{
    public class MinimumExperienceRequired : IAuthorizationRequirement
    {

        public int MinimumExperience { get; }

        public MinimumExperienceRequired(int minimumExperience)
        {
            MinimumExperience = minimumExperience;
        }
    }
}
