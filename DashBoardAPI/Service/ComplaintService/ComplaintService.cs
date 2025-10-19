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

        public List<JsonResponseEntity> GetComplaintDetailsByUserIdAndRoleId(long UserId, long RoleId)
        {
            var responses = new List<JsonResponseEntity>();

            try
            {
                using (var authCommand = new SqlCommand("stpGetAllComplaintsDetails"))
                {
                    authCommand.CommandType = CommandType.StoredProcedure;
                    authCommand.Parameters.AddWithValue("@UserId", UserId);
                    authCommand.Parameters.AddWithValue("@RoleId", RoleId);

                    var complaints = _complaintRepository.GetRecords(authCommand).ToList();

                    foreach (var item in complaints)
                    {
                        responses.Add(new JsonResponseEntity
                        {
                            Status = ApiStatus.OK,
                            Message = "Complaint fetched successfully.",
                            Data = item
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                responses.Add(new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while fetching complaints: " + ex.Message,
                    Data = null
                });
            }

            return responses;
        }

        public JsonResponseEntity InsertComplaintDetails(ComplaintEntity Data)
        {
            try
            {
              
                var authCommand = new SqlCommand("stp_Insertintocomplaintmaster");
                authCommand.CommandType = CommandType.StoredProcedure;
                authCommand.Parameters.AddWithValue("@CustomerId", Data.CustomerId);
                authCommand.Parameters.AddWithValue("@NatureOfComplaint", Data.NatureOfComplaint);
                authCommand.Parameters.AddWithValue("@Complaintdetails", Data.Complaintdetails);
                authCommand.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);

                var response = _complaintRepository.ExecuteProcedure(authCommand);

                return new JsonResponseEntity
                {
                    Status = ApiStatus.Success,
                    Message = "Complaint registered successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                // Log exception here
                return new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while inserting complaint: " + ex.Message,
                    Data = null
                };
            }
        }

       
    }
}
