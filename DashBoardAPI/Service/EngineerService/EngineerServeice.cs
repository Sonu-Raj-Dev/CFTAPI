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

        public JsonResponseEntity GetEngineerDetails()
        {
            var response = new JsonResponseEntity();

            try
            {
                // Step 1: Authenticate user
                var authCommand = new SqlCommand("stpGetEngineerDetails");
                authCommand.CommandType = CommandType.StoredProcedure;

                var Customer = _engineerRepository.GetRecord(authCommand);


                response.Status = ApiStatus.OK;
                response.Message = "Customer Fetched";
                response.Data = Customer;
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
