using BusinessLogics.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.AuthorizeHandler
{
    public class RequiredPermissionHandler : AuthorizationHandler<RequiredPermissionRequirement>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RequiredPermissionHandler(IUserRoleRepository userRoleRepository, IPermissionRepository permissionRepository)
        {
            _userRoleRepository = userRoleRepository;
            _permissionRepository = permissionRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequiredPermissionRequirement requirement)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Task.CompletedTask; // Exit early if no userIdClaim
            }

            var roles = _userRoleRepository.GetRolesOfUser(Guid.Parse(userIdClaim));

            foreach (var role in roles)
            {
                var permissions = _permissionRepository.GetPermissionsOfRole(role);
                if (permissions.Any(x => x.Value == requirement.Permission))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask; // No need to continue checking
                }
            }

            return Task.CompletedTask;
        }
    }

}
