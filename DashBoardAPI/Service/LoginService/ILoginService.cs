using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.LoginService
{
    public interface ILoginService
    {
        JsonResponseEntity GetUserDetailsByEmailAndPassword(string Email,string Password);
        JsonResponseEntity GetPermissionByUserId(Int64 UserId);
    }
}
