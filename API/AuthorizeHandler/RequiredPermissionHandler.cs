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
            var userIdClaim = context.User.FindFirst("userId").Value;
            if(userIdClaim == null)
            {
                return Task.CompletedTask;
            }
            var roles = _userRoleRepository.GetRolesOfUser(new Guid(userIdClaim));
            foreach (var role in roles)
            {
                var permissions = _permissionRepository.GetPermissonsOfRole(role);
                if(permissions.Select(x => x.Value).ToList().Contains(requirement.Permission))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
