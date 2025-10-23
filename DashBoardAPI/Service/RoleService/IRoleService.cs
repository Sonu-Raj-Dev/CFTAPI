using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.RoleService
{
    public interface IRoleService
    {
        List<JsonResponseEntity> GetRoles();
    }
}
