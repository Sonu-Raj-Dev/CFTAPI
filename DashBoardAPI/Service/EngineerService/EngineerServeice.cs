using DashBoardAPI.Entity;
using DashBoardAPI.Repository;
using System.Data;
using System.Data.SqlClient;

namespace DashBoardAPI.Service.EngineerService
{
    public class EngineerServeice:IEngineerService
    {
        private readonly IRepository<EngineerEntity> _engineerRepository;

        public EngineerServeice(IRepository<EngineerEntity> engineerRepository)
        {
            _engineerRepository = engineerRepository;

        }

    
        public List<JsonResponseEntity> GetEngineerDetails()
        {
            var responses = new List<JsonResponseEntity>();

            try
            {
                using (var authCommand = new SqlCommand("stpGetEngineerDetails"))
                {
                    authCommand.CommandType = CommandType.StoredProcedure;

                    var complaints = _engineerRepository.GetRecords(authCommand).ToList();

                    foreach (var item in complaints)
                    {
                        responses.Add(new JsonResponseEntity
                        {
                            Status = ApiStatus.OK,
                            Message = "Engineers fetched successfully.",
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
                    Message = "Error occurred while fetching Engineers: " + ex.Message,
                    Data = null
                });
            }

            return responses;
        }
        public JsonResponseEntity InsertUpdateEnginerMaster(EngineerEntity customer)
        {

            var responses = new JsonResponseEntity();

            try
            {

                var authCommand = new SqlCommand("stpInsertUpdateEngineerMaster");
                authCommand.CommandType = CommandType.StoredProcedure;
                authCommand.Parameters.AddWithValue("@Id", customer.Id);
                authCommand.Parameters.AddWithValue("@Name", customer.Name);
                authCommand.Parameters.AddWithValue("@Mobile", customer.MobileNumber);
                authCommand.Parameters.AddWithValue("@Email", customer.Email);       
                authCommand.Parameters.AddWithValue("@IsActive", customer.IsActive);
                var complaints = _engineerRepository.GetRecords(authCommand).ToList();


                responses.Status = ApiStatus.OK;
                responses.Message = "Enginer Created successfully.";
                responses.Data = complaints;

            }
            catch (Exception ex)
            {

                responses.Status = ApiStatus.Error;
                responses.Message = "Error occurred while fetching Enginer: " + ex.Message;
                responses.Data = null;
            }

            return responses;
        }


    }
}
