using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;

namespace API.AuthorizeHandler
{
    public class RequiredPermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }
        public RequiredPermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
