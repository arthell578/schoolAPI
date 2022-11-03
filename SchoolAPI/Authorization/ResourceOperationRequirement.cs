using Microsoft.AspNetCore.Authorization;

namespace SchoolAPI.Authorization
{
    public enum ResourceOperation
    {
        Create,Read,Update,Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation Operation { get; set; }
    }
}
