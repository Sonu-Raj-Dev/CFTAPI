using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using System.Data;
using System.Data.SqlClient;

namespace DashBoardAPI.Service.ComplaintService
{
    public class ComplaintService:IComplaintService
    {
        private readonly IRepository<ComplaintEntity> _complaintRepository;

        public ComplaintService(IRepository<ComplaintEntity> complaintRepository)
        {
            _complaintRepository = complaintRepository;

        }

        public JsonResponseEntity GetComplaintDetailsByUserIdAndRoleId(Int64 UserId,Int64 RoleId)
        {
            var response = new JsonResponseEntity();

            try
            {
                // Step 1: Authenticate user
                var authCommand = new SqlCommand("stpGetAllComplaintsDetails");
                authCommand.CommandType = CommandType.StoredProcedure;
                authCommand.Parameters.AddWithValue("@UserId", UserId);
                authCommand.Parameters.AddWithValue("@RoleId", RoleId);

                var Complaint = _complaintRepository.GetRecord(authCommand);


                response.Status = ApiStatus.OK;
                response.Message = "Complaint Fetched";
                response.Data = Complaint;
            }
            catch (Exception ex)
            {
                response.Status = ApiStatus.Error;
                response.Message = "Error occurred while logging in: " + ex.Message;
            }

            return response;
        }

    }
}
