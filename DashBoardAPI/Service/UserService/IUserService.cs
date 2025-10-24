using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.UserService
{
    public interface IUserService
    {
        List<JsonResponseEntity> GetUserDetails();
        JsonResponseEntity InsertUpdateUserMaster(UserEntity users);
        JsonResponseEntity InsertUpdateUserRolePermission(UserEntity users);

        List<JsonResponseEntity> GetUserRoleDetails();
    }
}
