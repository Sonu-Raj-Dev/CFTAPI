using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using System.Data;
using System.Data.SqlClient;

namespace DashBoardAPI.Service.ComplaintService
{
    public class ComplaintService:IComplaintService
    {
        private readonly IRepository<ComplaintEntity> _complaintRepository;
        private readonly IRepository<EmailEntity> _emailRepository;

        public ComplaintService(IRepository<ComplaintEntity> complaintRepository, IRepository<EmailEntity> emailRepository)
        {
            _complaintRepository = complaintRepository;
            _emailRepository = emailRepository;
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
                authCommand.Parameters.AddWithValue("@Id", Data.Id);
                authCommand.Parameters.AddWithValue("@CustomerId", Data.CustomerId);
                authCommand.Parameters.AddWithValue("@NatureOfComplaint", Data.NatureOfComplaint);
                authCommand.Parameters.AddWithValue("@Complaintdetails", Data.Complaintdetails);
                authCommand.Parameters.AddWithValue("@CreatedBy", Data.CreatedBy);
                authCommand.Parameters.AddWithValue("@StatusId", Data.StatusId);
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
        
     public JsonResponseEntity AssignEngineerToComplaint(ComplaintEntity Data)
        {
            try
            {

                var authCommand = new SqlCommand("stpAssignEngineertocomplaints");
                authCommand.CommandType = CommandType.StoredProcedure;
                authCommand.Parameters.AddWithValue("@Id", Data.Id);
                authCommand.Parameters.AddWithValue("@EngineerId", Data.EngineerId);

                var response = _complaintRepository.ExecuteProcedure(authCommand);

                return new JsonResponseEntity
                {
                    Status = ApiStatus.Success,
                    Message = "Engineer assigner successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                // Log exception here
                return new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while assigning engineer: " + ex.Message,
                    Data = null
                };
            }
        }
        public JsonResponseEntity DeleteComplaint(Int64 Id)
        {
            try
            {

                var authCommand = new SqlCommand("stpDeletecomplaints");
                authCommand.CommandType = CommandType.StoredProcedure;
                authCommand.Parameters.AddWithValue("@Id", Id);
              
                var response = _complaintRepository.ExecuteProcedure(authCommand);

                return new JsonResponseEntity
                {
                    Status = ApiStatus.Success,
                    Message = "Complaint deleted successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                // Log exception here
                return new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while Complaint deletion: " + ex.Message,
                    Data = null
                };
            }
        }

        public JsonResponseEntity GetEmailDetailByComplaintId(Int64 Id)
        {

            try
            {

                var authCommand = new SqlCommand("stpGetEmailTemplateDetailsById");
                authCommand.CommandType = CommandType.StoredProcedure;
                authCommand.Parameters.AddWithValue("@Id", Id);
            
                var response = _emailRepository.GetRecord(authCommand);

                return new JsonResponseEntity
                {
                    Status = ApiStatus.Success,
                    Message = "Engineer assigner successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                // Log exception here
                return new JsonResponseEntity
                {
                    Status = ApiStatus.Error,
                    Message = "Error occurred while assigning engineer: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
