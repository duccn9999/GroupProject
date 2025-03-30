namespace BusinessLogics.Repositories
{
    public interface IUserRoleRepository
    {
        public void UpdateRolesOfUser(Guid userId, List<Guid> roleIds);
        public List<Guid> GetRolesOfUser(Guid userId);
    }
}
