using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.LoginService
{
    public interface ILoginService
    {
        JsonResponseEntity GetUserDetailsByEmailAndPassword(string Email,string Password);
        JsonResponseEntity GetPermissionByUserId(Int64 UserId);
        List<JsonResponseEntity> GetAllPermissions();

        List<JsonResponseEntity> GetPermissionsByRoleId(Int64 RoleId);
    }
}
