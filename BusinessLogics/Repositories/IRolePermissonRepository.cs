namespace BusinessLogics.Repositories
{
    public interface IRolePermissonRepository
    {
        public void UpdateRolePermissionOfRole(Guid roleId, List<Guid> permissionIds);
    }
}
