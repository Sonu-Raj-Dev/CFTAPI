using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.UserService
{
    public interface IUserService
    {
        List<JsonResponseEntity> GetUserDetails();
    }
}
