using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.ComplaintService
{
    public interface IComplaintService
    {
        List<JsonResponseEntity> GetComplaintDetailsByUserIdAndRoleId(long UserId, long RoleId);
    }
}
