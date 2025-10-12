using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.ComplaintService
{
    public interface IComplaintService
    {
        JsonResponseEntity GetComplaintDetailsByUserIdAndRoleId(Int64 UserId,Int64 RoleId);

    }
}
