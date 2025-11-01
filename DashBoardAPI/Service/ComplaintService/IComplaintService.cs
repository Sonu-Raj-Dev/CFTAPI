using DashBoardAPI.Entity;

namespace DashBoardAPI.Service.ComplaintService
{
    public interface IComplaintService
    {
        List<JsonResponseEntity> GetComplaintDetailsByUserIdAndRoleId(long UserId, long RoleId);
        JsonResponseEntity InsertComplaintDetails(ComplaintEntity Data);
        JsonResponseEntity AssignEngineerToComplaint(ComplaintEntity Data);
        JsonResponseEntity DeleteComplaint(Int64 Id);
        JsonResponseEntity GetEmailDetailByComplaintId(Int64 Id);

        List<JsonResponseEntity> GetNatureOfComplaint();
    }
}
